using ERP.DOMAINMODELS.Dtos.Payment;

namespace ERP.APPLICATIONBUSINESS.Interfaces
{
    public interface IPaymentService
    {
        Task<IEnumerable<PaymentDto>> GetAllAsync();

        Task<PaymentDto?> GetByIdAsync(int id);

        Task<PaymentDto> CreateAsync(CreatePaymentDto dto);

        Task<PaymentDto?> UpdateAsync(
            int id,
            CreatePaymentDto dto);

        Task<bool> DeleteAsync(int id);
    }
}