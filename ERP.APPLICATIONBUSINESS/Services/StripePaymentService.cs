using ERP.APPLICATIONBUSINESS.Interfaces;
using ERP.APPLICATIONBUSINESS.Interfaces.IRepository;
using ERP.APPLICATIONBUSINESS.Settings;
using ERP.DOMAINMODELS.Dtos;
using ERP.DOMAINMODELS.Dtos.Payment;
using ERP.DOMAINMODELS.Entities;
using Microsoft.Extensions.Options;
using Stripe;
using Stripe.Checkout;
using System.Text.Json;

namespace ERP.APPLICATIONBUSINESS.Services
{
    public class StripePaymentService : IStripePaymentService
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IPaymentRepository _paymentRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly StripeSettings _stripeSettings;

        public StripePaymentService(
            IInvoiceRepository invoiceRepository,
            IPaymentRepository paymentRepository,
            ICustomerRepository customerRepository,
            IOptions<StripeSettings> stripeSettings)
        {
            _invoiceRepository = invoiceRepository;
            _paymentRepository = paymentRepository;
            _customerRepository = customerRepository;
            _stripeSettings = stripeSettings.Value;

            StripeConfiguration.ApiKey = _stripeSettings.SecretKey;
        }

        public async Task<StripeCheckoutResponseDto> CreateCheckoutSessionAsync(
            int invoiceId)
        {
            var invoice = await _invoiceRepository.GetByIdAsync(invoiceId);

            if (invoice == null)
            {
                throw new Exception("Invoice not found.");
            }

            if (!invoice.IsActive)
            {
                throw new Exception("Invoice is inactive.");
            }

            var customer =
                await _customerRepository.GetByIdAsync(invoice.CustomerId);

            if (customer == null)
            {
                throw new Exception("Customer not found.");
            }

            var paidAmount =
                await GetCompletedPaymentAmountAsync(invoiceId);

            var remainingAmount =
                invoice.TotalAmount - paidAmount;

            if (remainingAmount <= 0)
            {
                throw new Exception("Invoice is already fully paid.");
            }

            var options = new SessionCreateOptions
            {
                Mode = "payment",

                SuccessUrl =
                    "http://localhost:5173/payments/stripe-success?session_id={CHECKOUT_SESSION_ID}",

                CancelUrl =
                    "http://localhost:5173/payments/stripe-cancel",

                CustomerEmail =
                    string.IsNullOrWhiteSpace(customer.Email)
                        ? null
                        : customer.Email,

                ClientReferenceId =
                    invoice.InvoiceId.ToString(),

                Metadata = new Dictionary<string, string>
                {
                    {
                        "invoice_id",
                        invoice.InvoiceId.ToString()
                    },
                    {
                        "customer_id",
                        invoice.CustomerId.ToString()
                    },
                    {
                        "invoice_number",
                        invoice.InvoiceNumber
                    }
                },

                LineItems = new List<SessionLineItemOptions>
                {
                    new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            Currency = "inr",

                            ProductData =
                                new SessionLineItemPriceDataProductDataOptions
                                {
                                    Name =
                                        $"Invoice {invoice.InvoiceNumber}"
                                },

                            UnitAmount =
                                Convert.ToInt64(
                                    Math.Round(
                                        remainingAmount * 100,
                                        MidpointRounding.AwayFromZero))
                        },

                        Quantity = 1
                    }
                }
            };

            var service = new SessionService();

            var session =
                await service.CreateAsync(options);

            return new StripeCheckoutResponseDto
            {
                SessionId = session.Id,
                CheckoutUrl = session.Url
            };
        }

        public async Task HandleWebhookAsync(
            string json,
            string stripeSignature)
        {
            Stripe.Event stripeEvent;

            try
            {
                stripeEvent =
                    EventUtility.ConstructEvent(
                        json,
                        stripeSignature,
                        _stripeSettings.WebhookSecret);
            }
            catch (StripeException)
            {
                throw new Exception(
                    "Invalid Stripe webhook signature.");
            }

            if (stripeEvent.Type ==
                EventTypes.CheckoutSessionCompleted)
            {
                var session =
                    stripeEvent.Data.Object as Session;

                if (session == null)
                {
                    return;
                }

                await ProcessSuccessfulPaymentAsync(session);
            }
        }

        private async Task ProcessSuccessfulPaymentAsync(
            Session session)
        {
            if (session.Metadata == null ||
                !session.Metadata.ContainsKey("invoice_id"))
            {
                return;
            }

            var invoiceId =
                int.Parse(session.Metadata["invoice_id"]);

            var invoice =
                await _invoiceRepository.GetByIdAsync(invoiceId);

            if (invoice == null)
            {
                return;
            }

            /*
             * Prevent duplicate payment creation.
             */

            var existingPayments =
                await _paymentRepository.GetAllAsync();

            var alreadyProcessed =
                existingPayments.Any(x =>
                    x.GatewayPaymentId == session.PaymentIntentId ||
                    x.GatewayOrderId == session.Id);

            if (alreadyProcessed)
            {
                return;
            }

            var amount =
                session.AmountTotal.HasValue
                    ? session.AmountTotal.Value / 100m
                    : 0m;

            if (amount <= 0)
            {
                return;
            }

            var payment = new Payment
            {
                PaymentNumber =
                    $"PAY-{DateTime.UtcNow:yyyyMMddHHmmssfff}",

                InvoiceId = invoice.InvoiceId,

                CustomerId = invoice.CustomerId,

                PaymentDate = DateTime.UtcNow,

                Amount = amount,

                PaymentMethod = "Online",

                PaymentGateway = "Stripe",

                TransactionId = session.PaymentIntentId,

                GatewayOrderId = session.Id,

                GatewayPaymentId = session.PaymentIntentId,

                GatewayResponse =
                    JsonSerializer.Serialize(session),

                Status = "Completed",

                Notes = "Stripe Checkout payment",

                IsActive = true,

                CreatedAt = DateTime.UtcNow
            };

            await _paymentRepository.AddAsync(payment);

            invoice.Status =
                await CalculateInvoiceStatusAsync(
                    invoice.InvoiceId,
                    invoice.TotalAmount,
                    amount);

            await _invoiceRepository.UpdateAsync(invoice);
        }

        private async Task<decimal>
            GetCompletedPaymentAmountAsync(int invoiceId)
        {
            var payments =
                await _paymentRepository.GetAllAsync();

            return payments
                .Where(x =>
                    x.InvoiceId == invoiceId &&
                    x.IsActive &&
                    x.Status == "Completed")
                .Sum(x => x.Amount);
        }

        private async Task<string>
            CalculateInvoiceStatusAsync(
                int invoiceId,
                decimal invoiceTotal,
                decimal currentPaymentAmount)
        {
            var payments =
                await _paymentRepository.GetAllAsync();

            var paid =
                payments
                    .Where(x =>
                        x.InvoiceId == invoiceId &&
                        x.IsActive &&
                        x.Status == "Completed")
                    .Sum(x => x.Amount);

            /*
             * Current payment may not yet be visible
             * depending on repository SaveChanges implementation.
             */
            paid += currentPaymentAmount;

            if (paid >= invoiceTotal)
            {
                return "Paid";
            }

            if (paid > 0)
            {
                return "Partially Paid";
            }

            return "Pending";
        }
    }
}