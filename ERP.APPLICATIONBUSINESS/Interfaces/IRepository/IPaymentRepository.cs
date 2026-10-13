using ERP.DOMAINMODELS.Entities;

namespace ERP.APPLICATIONBUSINESS.Interfaces
{
    public interface IPaymentRepository
    {
        Task<IEnumerable<Payment>> GetAllAsync();

        Task<Payment?> GetByIdAsync(int id);

        Task<IEnumerable<Payment>> GetByInvoiceIdAsync(int invoiceId);

        Task<IEnumerable<Payment>> GetByCustomerIdAsync(int customerId);

        Task<decimal> GetTotalPaidForInvoiceAsync(int invoiceId);

        Task AddAsync(Payment payment);

        Task UpdateAsync(Payment payment);

        Task DeleteAsync(Payment payment);

        Task<bool> ExistsAsync(int id);

        Task SaveChangesAsync();
    }
}