using ERP.APPLICATIONBUSINESS.Interfaces.IRepository;
using ERP.APPLICATIONBUSINESS.Interfaces.IServices;
using ERP.DOMAINMODELS.Dtos;
using ERP.DOMAINMODELS.Entities;

namespace ERP.APPLICATIONBUSINESS.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly ISupplierRepository _repository;

        public SupplierService(ISupplierRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<SupplierDto>> GetAllAsync()
        {
            var suppliers = await _repository.GetAllAsync();

            return suppliers.Select(MapToDto).ToList();
        }

        public async Task<SupplierDto?> GetByIdAsync(int id)
        {
            var supplier = await _repository.GetByIdAsync(id);

            if (supplier == null)
                return null;

            return MapToDto(supplier);
        }

        public async Task<SupplierDto> CreateAsync(SupplierDto dto)
        {
            var supplier = new Supplier
            {
                SupplierCode = dto.SupplierCode,
                CompanyName = dto.CompanyName,
                ContactPerson = dto.ContactPerson,
                Email = dto.Email,
                Phone = dto.Phone,
                GSTNumber = dto.GSTNumber,
                Address = dto.Address,
                City = dto.City,
                State = dto.State,
                Country = dto.Country,
                PostalCode = dto.PostalCode,
                PaymentTerms = dto.PaymentTerms,
                BankName = dto.BankName,
                BankAccountNumber = dto.BankAccountNumber,
                IFSCCode = dto.IFSCCode,
                IsActive = true,
                CreatedDate = DateTime.UtcNow
            };

            var result = await _repository.AddAsync(supplier);

            return MapToDto(result);
        }

        public async Task<bool> UpdateAsync(int id, SupplierDto dto)
        {
            var supplier = await _repository.GetByIdAsync(id);

            if (supplier == null)
                return false;

            supplier.SupplierCode = dto.SupplierCode;
            supplier.CompanyName = dto.CompanyName;
            supplier.ContactPerson = dto.ContactPerson;
            supplier.Email = dto.Email;
            supplier.Phone = dto.Phone;
            supplier.GSTNumber = dto.GSTNumber;
            supplier.Address = dto.Address;
            supplier.City = dto.City;
            supplier.State = dto.State;
            supplier.Country = dto.Country;
            supplier.PostalCode = dto.PostalCode;
            supplier.PaymentTerms = dto.PaymentTerms;
            supplier.BankName = dto.BankName;
            supplier.BankAccountNumber = dto.BankAccountNumber;
            supplier.IFSCCode = dto.IFSCCode;
            supplier.IsActive = dto.IsActive;
            supplier.UpdatedDate = DateTime.UtcNow;

            await _repository.UpdateAsync(supplier);

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var supplier = await _repository.GetByIdAsync(id);

            if (supplier == null)
                return false;

            await _repository.DeleteAsync(id);

            return true;
        }

        private static SupplierDto MapToDto(Supplier supplier)
        {
            return new SupplierDto
            {
                Id = supplier.Id,
                SupplierCode = supplier.SupplierCode,
                CompanyName = supplier.CompanyName,
                ContactPerson = supplier.ContactPerson,
                Email = supplier.Email,
                Phone = supplier.Phone,
                GSTNumber = supplier.GSTNumber,
                Address = supplier.Address,
                City = supplier.City,
                State = supplier.State,
                Country = supplier.Country,
                PostalCode = supplier.PostalCode,
                PaymentTerms = supplier.PaymentTerms,
                BankName = supplier.BankName,
                BankAccountNumber = supplier.BankAccountNumber,
                IFSCCode = supplier.IFSCCode,
                IsActive = supplier.IsActive
            };
        }
    }
}