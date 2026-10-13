using ERP.DOMAINMODELS.Entities;

namespace ERP.APPLICATIONBUSINESS.Interfaces.IRepository
{
    public interface IInvoiceRepository
    {
        Task<List<Invoice>> GetAllAsync();

        Task<Invoice?> GetByIdAsync(int id);

        Task<Invoice> CreateAsync(Invoice invoice);

        Task<Invoice> UpdateAsync(Invoice invoice);

        Task<bool> DeleteAsync(int id);
    }
}