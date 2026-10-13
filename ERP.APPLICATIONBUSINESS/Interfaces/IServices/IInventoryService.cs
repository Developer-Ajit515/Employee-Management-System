using ERP.DOMAINMODELS.Dtos;
using ERP.DOMAINMODELS.Dtos;
using ERP.DOMAINMODELS.Entities;

namespace ERP.APPLICATIONBUSINESS.Interfaces.IServices
{
    public interface IInventoryService
    {
        Task<IEnumerable<InventoryDto>> GetAllAsync();

        Task<InventoryDto?> GetByIdAsync(int id);

        Task<IEnumerable<InventoryDto>> GetByProductIdAsync(int productId);

        Task<InventoryDto> StockInAsync(CreateStockInDto dto);

        Task<InventoryDto> StockOutAsync(CreateStockOutDto dto);

        Task<InventoryDto> StockAdjustmentAsync(
            CreateStockAdjustmentDto dto);
        Task<bool> DeleteAsync(int id);
        
    }
}