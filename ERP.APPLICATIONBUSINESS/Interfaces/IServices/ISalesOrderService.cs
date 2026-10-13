using ERP.DOMAINMODELS.Dtos;

namespace ERP.APPLICATIONBUSINESS.Interfaces.IServices
{
    public interface ISalesOrderService
    {
        Task<IEnumerable<SalesOrderDto>> GetAllAsync();

        Task<SalesOrderDto?> GetByIdAsync(int id);

        Task<SalesOrderDto> CreateAsync(
            CreateSalesOrderDto dto);

        Task<SalesOrderDto?> UpdateAsync(
            int id,
            CreateSalesOrderDto dto);

        Task<bool> DeleteAsync(int id);
    }
}