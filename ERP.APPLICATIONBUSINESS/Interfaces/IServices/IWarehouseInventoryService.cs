
using ERP.DOMAINMODELS.Dtos;

namespace ERP.APPLICATIONBUSINESS.Interfaces.IServices
{
    public interface IWarehouseInventoryService
    {
        Task<List<WarehouseInventoryDto>>
            GetAllAsync();

        Task<List<WarehouseInventoryDto>>
            GetByWarehouseIdAsync(int warehouseId);

        Task<WarehouseInventoryDto?>
            GetByIdAsync(int id);

        Task<WarehouseInventoryDto>
            AddAsync(CreateWarehouseInventoryDto dto);

        Task<bool>
            UpdateAsync(UpdateWarehouseInventoryDto dto);

        Task<bool>
            DeleteAsync(int id);
    }
}

