using ERP.APPLICATIONBUSINESS.Interfaces.IRepository;
using ERP.APPLICATIONBUSINESS.Interfaces.IServices;
using ERP.DOMAINMODELS.Dtos;
using ERP.DOMAINMODELS.Entities;

namespace ERP.APPLICATIONBUSINESS.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _repository;

        public CustomerService(ICustomerRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<CustomerDto>> GetAllAsync()
        {
            var customers = await _repository.GetAllAsync();

            return customers.Select(x => new CustomerDto
            {
                Id = x.Id,
                Name = x.Name,
                Email = x.Email,
                Phone = x.Phone,
                Address = x.Address,
                IsActive = x.IsActive
            });
        }

        public async Task<CustomerDto?> GetByIdAsync(int id)
        {
            var customer = await _repository.GetByIdAsync(id);

            if (customer == null)
                return null;

            return new CustomerDto
            {
                Id = customer.Id,
                Name = customer.Name,
                Email = customer.Email,
                Phone = customer.Phone,
                Address = customer.Address,
                IsActive = customer.IsActive
            };
        }

        public async Task<CustomerDto> CreateAsync(
            CreateCustomerDto dto)
        {
            var customer = new Customer
            {
                Name = dto.Name,
                Email = dto.Email,
                Phone = dto.Phone,
                Address = dto.Address,
                IsActive = dto.IsActive
            };

            var result = await _repository.AddAsync(customer);

            return new CustomerDto
            {
                Id = result.Id,
                Name = result.Name,
                Email = result.Email,
                Phone = result.Phone,
                Address = result.Address,
                IsActive = result.IsActive
            };
        }

        public async Task<CustomerDto?> UpdateAsync(
            int id,
            CreateCustomerDto dto)
        {
            var customer = new Customer
            {
                Id = id,
                Name = dto.Name,
                Email = dto.Email,
                Phone = dto.Phone,
                Address = dto.Address,
                IsActive = dto.IsActive
            };

            var result = await _repository.UpdateAsync(customer);

            if (result == null)
                return null;

            return new CustomerDto
            {
                Id = result.Id,
                Name = result.Name,
                Email = result.Email,
                Phone = result.Phone,
                Address = result.Address,
                IsActive = result.IsActive
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}