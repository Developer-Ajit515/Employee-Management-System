using ERP.DOMAINMODELS.Dtos;

namespace ERP.APPLICATIONBUSINESS.Interfaces.IServices
{
    public interface IWarehouseService
    {
        Task<IEnumerable<object>> GetAllAsync();

        Task<object?> GetByIdAsync(int id);

        Task<object> AddAsync(
            CreateWarehouseDto dto
        );

        Task<bool> UpdateAsync(
            UpdateWarehouseDto dto
        );

        Task<bool> DeleteAsync(int id);
    }
}