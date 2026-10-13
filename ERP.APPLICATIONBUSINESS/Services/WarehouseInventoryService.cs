
using ERP.APPLICATIONBUSINESS.Interfaces.IRepository;
using ERP.APPLICATIONBUSINESS.Interfaces.IServices;
using ERP.DOMAINMODELS.Dtos;
using ERP.DOMAINMODELS.Entities;

namespace ERP.APPLICATIONBUSINESS.Services
{
    public class WarehouseInventoryService
        : IWarehouseInventoryService
    {
        private readonly IWarehouseInventoryRepository _repository;

        public WarehouseInventoryService(
            IWarehouseInventoryRepository repository)
        {
            _repository = repository;
        }

        // ==========================================
        // GET ALL
        // ==========================================
        public async Task<List<WarehouseInventoryDto>>
            GetAllAsync()
        {
            var data =
                await _repository.GetAllAsync();

            return data.Select(MapToDto).ToList();
        }

        // ==========================================
        // GET BY WAREHOUSE
        // ==========================================
        public async Task<List<WarehouseInventoryDto>>
            GetByWarehouseIdAsync(int warehouseId)
        {
            var data =
                await _repository
                    .GetByWarehouseIdAsync(warehouseId);

            return data.Select(MapToDto).ToList();
        }

        // ==========================================
        // GET BY ID
        // ==========================================
        public async Task<WarehouseInventoryDto?>
            GetByIdAsync(int id)
        {
            var data =
                await _repository.GetByIdAsync(id);

            if (data == null)
            {
                return null;
            }

            return MapToDto(data);
        }

        // ==========================================
        // CREATE
        // ==========================================
        public async Task<WarehouseInventoryDto>
            AddAsync(CreateWarehouseInventoryDto dto)
        {
            if (dto.Quantity < 0)
            {
                throw new ArgumentException(
                    "Quantity cannot be negative.");
            }

            if (dto.ReorderLevel < 0)
            {
                throw new ArgumentException(
                    "Reorder level cannot be negative.");
            }

            var existing =
                await _repository
                    .GetByWarehouseAndProductAsync(
                        dto.WarehouseId,
                        dto.ProductId);

            if (existing != null)
            {
                throw new InvalidOperationException(
                    "This product is already mapped to this warehouse.");
            }

            var entity = new WarehouseInventory
            {
                WarehouseId = dto.WarehouseId,
                ProductId = dto.ProductId,
                Quantity = dto.Quantity,
                ReorderLevel = dto.ReorderLevel,
                UpdatedAt = DateTime.UtcNow
            };

            var created =
                await _repository.AddAsync(entity);

            var result =
                await _repository.GetByIdAsync(created.Id);

            return MapToDto(result!);
        }

        // ==========================================
        // UPDATE
        // ==========================================
        public async Task<bool>
            UpdateAsync(UpdateWarehouseInventoryDto dto)
        {
            if (dto.Quantity < 0)
            {
                throw new ArgumentException(
                    "Quantity cannot be negative.");
            }

            if (dto.ReorderLevel < 0)
            {
                throw new ArgumentException(
                    "Reorder level cannot be negative.");
            }

            var existing =
                await _repository.GetByIdAsync(dto.Id);

            if (existing == null)
            {
                return false;
            }

            var duplicate =
                await _repository
                    .GetByWarehouseAndProductAsync(
                        dto.WarehouseId,
                        dto.ProductId);

            if (duplicate != null &&
                duplicate.Id != dto.Id)
            {
                throw new InvalidOperationException(
                    "This product is already mapped to this warehouse.");
            }

            var entity = new WarehouseInventory
            {
                Id = dto.Id,
                WarehouseId = dto.WarehouseId,
                ProductId = dto.ProductId,
                Quantity = dto.Quantity,
                ReorderLevel = dto.ReorderLevel,
                UpdatedAt = DateTime.UtcNow
            };

            return await _repository.UpdateAsync(entity);
        }

        // ==========================================
        // DELETE
        // ==========================================
        public async Task<bool>
            DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }

        // ==========================================
        // MAP ENTITY TO DTO
        // ==========================================
        private WarehouseInventoryDto MapToDto(
            WarehouseInventory entity)
        {
            return new WarehouseInventoryDto
            {
                Id = entity.Id,
                WarehouseId = entity.WarehouseId,
                ProductId = entity.ProductId,

                ProductName =
                    entity.Product?.Name,

                Quantity = entity.Quantity,

                ReorderLevel =
                    entity.ReorderLevel,

                UpdatedAt =
                    entity.UpdatedAt
            };
        }
    }
}

