using ERP.DOMAINMODELS.Entities;

namespace ERP.APPLICATIONBUSINESS.Interfaces.IRepository
{
    public interface ISupplierRepository
    {
        Task<List<Supplier>> GetAllAsync();

        Task<Supplier?> GetByIdAsync(int id);

        Task<Supplier> AddAsync(Supplier supplier);

        Task UpdateAsync(Supplier supplier);

        Task DeleteAsync(int id);
    }
}