using ERP.APPLICATIONBUSINESS.Interfaces.IRepository;
using ERP.APPLICATIONBUSINESS.Interfaces.IServices;
using ERP.DOMAINMODELS.Dtos;
using ERP.DOMAINMODELS.Entities;

namespace ERP.APPLICATIONBUSINESS.Services
{
    public class WarehouseService : IWarehouseService
    {
        private readonly IWarehouseRepository _repository;

        public WarehouseService(
            IWarehouseRepository repository)
        {
            _repository = repository;
        }

        // ==========================================
        // GET ALL
        // ==========================================
        public async Task<IEnumerable<object>> GetAllAsync()
        {
            var warehouses =
                await _repository.GetAllAsync();

            return warehouses.Select(x => new
            {
                x.Id,
                x.Name,
                x.Code,
                x.Description,
                x.Address,
                x.City,
                x.State,
                x.Country,
                x.PostalCode,
                x.Phone,
                x.Email,
                x.IsActive,
                x.CreatedAt,
                x.UpdatedAt
            });
        }

        // ==========================================
        // GET BY ID
        // ==========================================
        public async Task<object?> GetByIdAsync(int id)
        {
            var warehouse =
                await _repository.GetByIdAsync(id);

            if (warehouse == null)
            {
                return null;
            }

            return new
            {
                warehouse.Id,
                warehouse.Name,
                warehouse.Code,
                warehouse.Description,
                warehouse.Address,
                warehouse.City,
                warehouse.State,
                warehouse.Country,
                warehouse.PostalCode,
                warehouse.Phone,
                warehouse.Email,
                warehouse.IsActive,
                warehouse.CreatedAt,
                warehouse.UpdatedAt
            };
        }

        // ==========================================
        // CREATE
        // ==========================================
        public async Task<object> AddAsync(
            CreateWarehouseDto dto)
        {
            var warehouse = new Warehouse
            {
                Name = dto.Name,
                Code = dto.Code,
                Description = dto.Description,
                Address = dto.Address,
                City = dto.City,
                State = dto.State,
                Country = dto.Country,
                PostalCode = dto.PostalCode,
                Phone = dto.Phone,
                Email = dto.Email,
                IsActive = dto.IsActive,
                CreatedAt = DateTime.UtcNow
            };

            var created =
                await _repository.AddAsync(warehouse);

            return created;
        }

        // ==========================================
        // UPDATE
        // ==========================================
        public async Task<bool> UpdateAsync(
            UpdateWarehouseDto dto)
        {
            var warehouse = new Warehouse
            {
                Id = dto.Id,
                Name = dto.Name,
                Code = dto.Code,
                Description = dto.Description,
                Address = dto.Address,
                City = dto.City,
                State = dto.State,
                Country = dto.Country,
                PostalCode = dto.PostalCode,
                Phone = dto.Phone,
                Email = dto.Email,
                IsActive = dto.IsActive
            };

            return await _repository.UpdateAsync(
                warehouse
            );
        }

        // ==========================================
        // DELETE
        // ==========================================
        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}