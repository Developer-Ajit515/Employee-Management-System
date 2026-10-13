using ERP.APPLICATIONBUSINESS.Interfaces.IRepository;
using ERP.DOMAINMODELS.Entities;
using ERP.INFRASTRUCTUREDATA.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.INFRASTRUCTUREDATA.Repositories
{
    public class InventoryRepository : IInventoryRepository
    {
        private readonly AppDbContext _context;

        public InventoryRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Inventory>> GetAllAsync()
        {
            return await _context.Inventories
                .Include(x => x.Product)
                .AsNoTracking()
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();
        }

        public async Task<Inventory?> GetByIdAsync(int id)
        {
            return await _context.Inventories
                .Include(x => x.Product)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Inventory>> GetByProductIdAsync(int productId)
        {
            return await _context.Inventories
                .Include(x => x.Product)
                .AsNoTracking()
                .Where(x => x.ProductId == productId)
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();
        }

        public async Task<Inventory> AddAsync(Inventory inventory)
        {
            await _context.Inventories.AddAsync(inventory);
            await _context.SaveChangesAsync();

            return inventory;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var inventory = await _context.Inventories
                .FirstOrDefaultAsync(x => x.Id == id);

            if (inventory == null)
                return false;

            _context.Inventories.Remove(inventory);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdateAsync(Inventory inventory)
        {
            var existing = await _context.Inventories
                .FirstOrDefaultAsync(x => x.Id == inventory.Id);

            if (existing == null)
                return false;

            existing.ProductId = inventory.ProductId;
            existing.Quantity = inventory.Quantity;

            existing.TransactionType = inventory.TransactionType;
            existing.ReferenceType = inventory.ReferenceType;
            existing.ReferenceId = inventory.ReferenceId;
            existing.Remarks = inventory.Remarks;
            existing.CreatedAt = inventory.CreatedAt;
            existing.CreatedBy = inventory.CreatedBy;

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
