using ERP.DOMAINMODELS.Dtos;
using ERP.DOMAINMODELS.Dtos.Payment;

namespace ERP.APPLICATIONBUSINESS.Interfaces
{
    public interface IStripePaymentService
    {
        Task<StripeCheckoutResponseDto> CreateCheckoutSessionAsync(
            int invoiceId);

        Task HandleWebhookAsync(
            string json,
            string stripeSignature);
    }
}