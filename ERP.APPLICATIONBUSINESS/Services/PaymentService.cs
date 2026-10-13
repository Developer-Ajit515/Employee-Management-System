using ERP.APPLICATIONBUSINESS.Interfaces;
using ERP.APPLICATIONBUSINESS.Interfaces.IRepository;
using ERP.DOMAINMODELS.Dtos.Payment;
using ERP.DOMAINMODELS.Entities;

namespace ERP.APPLICATIONBUSINESS.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly ICustomerRepository _customerRepository;

        public PaymentService(
            IPaymentRepository paymentRepository,
            IInvoiceRepository invoiceRepository,
            ICustomerRepository customerRepository)
        {
            _paymentRepository = paymentRepository;
            _invoiceRepository = invoiceRepository;
            _customerRepository = customerRepository;
        }

        // =========================================================
        // GET ALL PAYMENTS
        // =========================================================

        public async Task<IEnumerable<PaymentDto>> GetAllAsync()
        {
            var payments =
                await _paymentRepository.GetAllAsync();

            var result = new List<PaymentDto>();

            foreach (var payment in payments)
            {
                var previousPaid =
                    await _paymentRepository
                        .GetTotalPaidForInvoiceAsync(
                            payment.InvoiceId);

                // Exclude current payment from previous paid
                if (payment.Status == "Completed")
                {
                    previousPaid -= payment.Amount;
                }

                var invoiceTotal =
                    payment.Invoice?.TotalAmount ?? 0m;

                result.Add(MapToDto(
                    payment,
                    invoiceTotal,
                    previousPaid));
            }

            return result;
        }


        // =========================================================
        // GET PAYMENT BY ID
        // =========================================================

        public async Task<PaymentDto?> GetByIdAsync(int id)
        {
            var payment =
                await _paymentRepository.GetByIdAsync(id);

            if (payment == null)
                return null;

            var previousPaid =
                await _paymentRepository
                    .GetTotalPaidForInvoiceAsync(
                        payment.InvoiceId);

            // Exclude current payment
            if (payment.Status == "Completed")
            {
                previousPaid -= payment.Amount;
            }

            var invoiceTotal =
                payment.Invoice?.TotalAmount ?? 0m;

            return MapToDto(
                payment,
                invoiceTotal,
                previousPaid);
        }


        // =========================================================
        // CREATE PAYMENT
        // =========================================================

        public async Task<PaymentDto> CreateAsync(
            CreatePaymentDto dto)
        {
            // ---------------------------------------------
            // Validate amount
            // ---------------------------------------------

            if (dto.Amount <= 0)
            {
                throw new Exception(
                    "Payment amount must be greater than zero.");
            }


            // ---------------------------------------------
            // Get invoice
            // ---------------------------------------------

            var invoice =
                await _invoiceRepository.GetByIdAsync(
                    dto.InvoiceId);

            if (invoice == null)
            {
                throw new Exception(
                    "Invoice not found.");
            }


            // ---------------------------------------------
            // Get customer
            // ---------------------------------------------

            var customer =
                await _customerRepository.GetByIdAsync(
                    dto.CustomerId);

            if (customer == null)
            {
                throw new Exception(
                    "Customer not found.");
            }


            // ---------------------------------------------
            // Validate invoice/customer relationship
            // ---------------------------------------------

            if (invoice.CustomerId != dto.CustomerId)
            {
                throw new Exception(
                    "Selected customer does not belong to the selected invoice.");
            }


            // ---------------------------------------------
            // Check completed payments
            // ---------------------------------------------

            if (dto.Status == "Completed")
            {
                var alreadyPaid =
                    await _paymentRepository
                        .GetTotalPaidForInvoiceAsync(
                            dto.InvoiceId);

                var remaining =
                    invoice.TotalAmount - alreadyPaid;

                if (remaining <= 0)
                {
                    throw new Exception(
                        "This invoice has already been fully paid.");
                }

                if (dto.Amount > remaining)
                {
                    throw new Exception(
                        $"Payment exceeds remaining invoice amount. " +
                        $"Remaining amount is {remaining:N2}.");
                }
            }


            // ---------------------------------------------
            // Create payment
            // ---------------------------------------------

            var payment = new Payment
            {
                PaymentNumber =
                    $"PAY-{DateTime.UtcNow:yyyyMMddHHmmssfff}",

                InvoiceId = dto.InvoiceId,

                CustomerId = dto.CustomerId,

                PaymentDate = dto.PaymentDate,

                Amount = dto.Amount,

                PaymentMethod =
                    dto.PaymentMethod,

                ReferenceNumber =
                    dto.ReferenceNumber,

                Status =
                    dto.Status,

                Notes =
                    dto.Notes,

                IsActive = true,

                CreatedAt = DateTime.UtcNow
            };


            // ---------------------------------------------
            // Save
            // ---------------------------------------------

            await _paymentRepository.AddAsync(payment);

            await _paymentRepository.SaveChangesAsync();


            // ---------------------------------------------
            // Update invoice status
            // ---------------------------------------------

            await UpdateInvoiceStatusAsync(
                invoice.InvoiceId);


            // ---------------------------------------------
            // Return created payment
            // ---------------------------------------------

            return (await GetByIdAsync(
                payment.PaymentId))!;
        }


        // =========================================================
        // UPDATE PAYMENT
        // =========================================================

        public async Task<PaymentDto?> UpdateAsync(
            int id,
            CreatePaymentDto dto)
        {
            // ---------------------------------------------
            // Validate amount
            // ---------------------------------------------

            if (dto.Amount <= 0)
            {
                throw new Exception(
                    "Payment amount must be greater than zero.");
            }


            // ---------------------------------------------
            // Get existing payment
            // ---------------------------------------------

            var payment =
                await _paymentRepository.GetByIdAsync(id);

            if (payment == null)
            {
                return null;
            }


            // ---------------------------------------------
            // Get invoice
            // ---------------------------------------------

            var invoice =
                await _invoiceRepository.GetByIdAsync(
                    dto.InvoiceId);

            if (invoice == null)
            {
                throw new Exception(
                    "Invoice not found.");
            }


            // ---------------------------------------------
            // Get customer
            // ---------------------------------------------

            var customer =
                await _customerRepository.GetByIdAsync(
                    dto.CustomerId);

            if (customer == null)
            {
                throw new Exception(
                    "Customer not found.");
            }


            // ---------------------------------------------
            // Validate customer/invoice
            // ---------------------------------------------

            if (invoice.CustomerId != dto.CustomerId)
            {
                throw new Exception(
                    "Selected customer does not belong to the selected invoice.");
            }


            // ---------------------------------------------
            // Validate payment amount
            // ---------------------------------------------

            if (dto.Status == "Completed")
            {
                var alreadyPaid =
                    await _paymentRepository
                        .GetTotalPaidForInvoiceAsync(
                            dto.InvoiceId);

                // Remove current payment from calculation
                if (payment.Status == "Completed" &&
                    payment.InvoiceId == dto.InvoiceId)
                {
                    alreadyPaid -= payment.Amount;
                }

                var remaining =
                    invoice.TotalAmount - alreadyPaid;

                if (remaining <= 0)
                {
                    throw new Exception(
                        "This invoice has already been fully paid.");
                }

                if (dto.Amount > remaining)
                {
                    throw new Exception(
                        $"Payment exceeds remaining invoice amount. " +
                        $"Remaining amount is {remaining:N2}.");
                }
            }


            // ---------------------------------------------
            // Update payment
            // ---------------------------------------------

            payment.InvoiceId =
                dto.InvoiceId;

            payment.CustomerId =
                dto.CustomerId;

            payment.PaymentDate =
                dto.PaymentDate;

            payment.Amount =
                dto.Amount;

            payment.PaymentMethod =
                dto.PaymentMethod;

            payment.ReferenceNumber =
                dto.ReferenceNumber;

            payment.Status =
                dto.Status;

            payment.Notes =
                dto.Notes;

            payment.UpdatedAt =
                DateTime.UtcNow;


            // ---------------------------------------------
            // Save
            // ---------------------------------------------

            await _paymentRepository.UpdateAsync(
                payment);

            await _paymentRepository.SaveChangesAsync();


            // ---------------------------------------------
            // Update invoice status
            // ---------------------------------------------

            await UpdateInvoiceStatusAsync(
                invoice.InvoiceId);


            return await GetByIdAsync(id);
        }


        // =========================================================
        // DELETE PAYMENT
        // =========================================================

        public async Task<bool> DeleteAsync(int id)
        {
            var payment =
                await _paymentRepository.GetByIdAsync(id);

            if (payment == null)
            {
                return false;
            }

            var invoiceId =
                payment.InvoiceId;


            // ---------------------------------------------
            // Soft delete
            // ---------------------------------------------

            await _paymentRepository.DeleteAsync(
                payment);

            await _paymentRepository.SaveChangesAsync();


            // ---------------------------------------------
            // Recalculate invoice status
            // ---------------------------------------------

            await UpdateInvoiceStatusAsync(
                invoiceId);


            return true;
        }


        // =========================================================
        // UPDATE INVOICE STATUS
        // =========================================================

        private async Task UpdateInvoiceStatusAsync(
            int invoiceId)
        {
            var invoice =
                await _invoiceRepository.GetByIdAsync(
                    invoiceId);

            if (invoice == null)
            {
                return;
            }


            var totalPaid =
                await _paymentRepository
                    .GetTotalPaidForInvoiceAsync(
                        invoiceId);


            if (totalPaid <= 0)
            {
                invoice.Status = "Pending";
            }
            else if (totalPaid < invoice.TotalAmount)
            {
                invoice.Status = "Partially Paid";
            }
            else
            {
                invoice.Status = "Paid";
            }


            await _invoiceRepository.UpdateAsync(
                invoice);
        }


        // =========================================================
        // MAP ENTITY TO DTO
        // =========================================================

        private PaymentDto MapToDto(
            Payment payment,
            decimal invoiceTotal,
            decimal previousPaid)
        {
            var remainingAmount =
                Math.Max(
                    0,
                    invoiceTotal -
                    previousPaid -
                    payment.Amount);

            return new PaymentDto
            {
                PaymentId =
                    payment.PaymentId,

                PaymentNumber =
                    payment.PaymentNumber,

                InvoiceId =
                    payment.InvoiceId,

                InvoiceNumber =
                    payment.Invoice?.InvoiceNumber,

                CustomerId =
                    payment.CustomerId,

                CustomerName =
                    payment.Customer?.Name,

                PaymentDate =
                    payment.PaymentDate,

                Amount =
                    payment.Amount,

                PaymentMethod =
                    payment.PaymentMethod,

                ReferenceNumber =
                    payment.ReferenceNumber,

                Status =
                    payment.Status,

                Notes =
                    payment.Notes,

                IsActive =
                    payment.IsActive,

                CreatedAt =
                    payment.CreatedAt,

                InvoiceTotal =
                    invoiceTotal,

                PreviouslyPaid =
                    previousPaid,

                RemainingAmount =
                    remainingAmount
            };
        }
    }
}