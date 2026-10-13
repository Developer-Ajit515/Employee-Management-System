using ERP.DOMAINMODELS.Entities;

namespace ERP.APPLICATIONBUSINESS.Interfaces.IRepository
{
    public interface IInventoryRepository
    {
        Task<IEnumerable<Inventory>> GetAllAsync();

        Task<Inventory?> GetByIdAsync(int id);

        Task<IEnumerable<Inventory>> GetByProductIdAsync(int productId);

        Task<Inventory> AddAsync(Inventory inventory);

        Task<bool> DeleteAsync(int id);
        Task<bool> UpdateAsync(Inventory inventory);
    }
}