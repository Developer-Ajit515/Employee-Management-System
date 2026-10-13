
using ERP.DOMAINMODELS.Entities;

namespace ERP.APPLICATIONBUSINESS.Interfaces.IRepository
{
    public interface IWarehouseInventoryRepository
    {
        Task<List<WarehouseInventory>> GetAllAsync();

        Task<List<WarehouseInventory>> GetByWarehouseIdAsync(
            int warehouseId);

        Task<WarehouseInventory?> GetByIdAsync(
            int id);

        Task<WarehouseInventory?> GetByWarehouseAndProductAsync(
            int warehouseId,
            int productId);

        Task<WarehouseInventory> AddAsync(
            WarehouseInventory entity);

        Task<bool> UpdateAsync(
            WarehouseInventory entity);

        Task<bool> DeleteAsync(
            int id);
    }
}

