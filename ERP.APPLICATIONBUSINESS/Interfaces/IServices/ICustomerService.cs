using ERP.DOMAINMODELS.Dtos;

namespace ERP.APPLICATIONBUSINESS.Interfaces.IServices
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerDto>> GetAllAsync();

        Task<CustomerDto?> GetByIdAsync(int id);

        Task<CustomerDto> CreateAsync(CreateCustomerDto dto);

        Task<CustomerDto?> UpdateAsync(
            int id,
            CreateCustomerDto dto);

        Task<bool> DeleteAsync(int id);
    }
}