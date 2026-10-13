
using ERP.APPLICATIONBUSINESS.Interfaces.IRepository;
using ERP.DOMAINMODELS.Entities;
using ERP.INFRASTRUCTUREDATA.Data;
using Microsoft.EntityFrameworkCore;

namespace ERP.INFRASTRUCTUREDATA.Repositories
{
    public class WarehouseInventoryRepository
        : IWarehouseInventoryRepository
    {
        private readonly AppDbContext _context;

        public WarehouseInventoryRepository(
            AppDbContext context)
        {
            _context = context;
        }

        // ==========================================
        // GET ALL
        // ==========================================
        public async Task<List<WarehouseInventory>> GetAllAsync()
        {
            return await _context.WarehouseInventories
                .Include(x => x.Warehouse)
                .Include(x => x.Product)
                .AsNoTracking()
                .ToListAsync();
        }

        // ==========================================
        // GET BY WAREHOUSE
        // ==========================================
        public async Task<List<WarehouseInventory>>
            GetByWarehouseIdAsync(int warehouseId)
        {
            return await _context.WarehouseInventories
                .Include(x => x.Warehouse)
                .Include(x => x.Product)
                .Where(x => x.WarehouseId == warehouseId)
                .AsNoTracking()
                .ToListAsync();
        }

        // ==========================================
        // GET BY ID
        // ==========================================
        public async Task<WarehouseInventory?>
            GetByIdAsync(int id)
        {
            return await _context.WarehouseInventories
                .Include(x => x.Warehouse)
                .Include(x => x.Product)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        // ==========================================
        // GET BY WAREHOUSE + PRODUCT
        // ==========================================
        public async Task<WarehouseInventory?>
            GetByWarehouseAndProductAsync(
                int warehouseId,
                int productId)
        {
            return await _context.WarehouseInventories
                .FirstOrDefaultAsync(x =>
                    x.WarehouseId == warehouseId &&
                    x.ProductId == productId);
        }

        // ==========================================
        // CREATE
        // ==========================================
        public async Task<WarehouseInventory> AddAsync(
            WarehouseInventory entity)
        {
            _context.WarehouseInventories.Add(entity);

            await _context.SaveChangesAsync();

            return entity;
        }

        // ==========================================
        // UPDATE
        // ==========================================
        public async Task<bool> UpdateAsync(
            WarehouseInventory entity)
        {
            var existing =
                await _context.WarehouseInventories
                    .FirstOrDefaultAsync(x =>
                        x.Id == entity.Id);

            if (existing == null)
            {
                return false;
            }

            existing.WarehouseId = entity.WarehouseId;
            existing.ProductId = entity.ProductId;
            existing.Quantity = entity.Quantity;
            existing.ReorderLevel = entity.ReorderLevel;
            existing.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return true;
        }

        // ==========================================
        // DELETE
        // ==========================================
        public async Task<bool> DeleteAsync(int id)
        {
            var entity =
                await _context.WarehouseInventories
                    .FirstOrDefaultAsync(x => x.Id == id);

            if (entity == null)
            {
                return false;
            }

            _context.WarehouseInventories.Remove(entity);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}

