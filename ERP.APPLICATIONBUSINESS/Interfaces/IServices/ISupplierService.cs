using ERP.DOMAINMODELS.Dtos;

namespace ERP.APPLICATIONBUSINESS.Interfaces.IServices
{
    public interface ISupplierService
    {
        Task<List<SupplierDto>> GetAllAsync();

        Task<SupplierDto?> GetByIdAsync(int id);

        Task<SupplierDto> CreateAsync(SupplierDto dto);

        Task<bool> UpdateAsync(int id, SupplierDto dto);

        Task<bool> DeleteAsync(int id);
    }
}