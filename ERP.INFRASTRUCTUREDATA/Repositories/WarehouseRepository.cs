using ERP.APPLICATIONBUSINESS.Interfaces.IRepository;
using ERP.DOMAINMODELS.Entities;
using ERP.INFRASTRUCTUREDATA.Data;
using Microsoft.EntityFrameworkCore;

namespace ERP.INFRASTRUCTUREDATA.Repositories
{
    public class WarehouseRepository : IWarehouseRepository
    {
        private readonly AppDbContext _context;

        public WarehouseRepository(AppDbContext context)
        {
            _context = context;
        }

        // ==========================================
        // GET ALL
        // ==========================================
        public async Task<IEnumerable<Warehouse>> GetAllAsync()
        {
            return await _context.Warehouses
                .AsNoTracking()
                .OrderByDescending(x => x.Id)
                .ToListAsync();
        }

        // ==========================================
        // GET BY ID
        // ==========================================
        public async Task<Warehouse?> GetByIdAsync(int id)
        {
            return await _context.Warehouses
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        // ==========================================
        // CREATE
        // ==========================================
        public async Task<Warehouse> AddAsync(
            Warehouse warehouse)
        {
            _context.Warehouses.Add(warehouse);

            await _context.SaveChangesAsync();

            return warehouse;
        }

        // ==========================================
        // UPDATE
        // ==========================================
        public async Task<bool> UpdateAsync(
            Warehouse warehouse)
        {
            var existing =
                await _context.Warehouses
                    .FirstOrDefaultAsync(
                        x => x.Id == warehouse.Id
                    );

            if (existing == null)
            {
                return false;
            }

            existing.Name = warehouse.Name;
            existing.Code = warehouse.Code;
            existing.Description = warehouse.Description;
            existing.Address = warehouse.Address;
            existing.City = warehouse.City;
            existing.State = warehouse.State;
            existing.Country = warehouse.Country;
            existing.PostalCode = warehouse.PostalCode;
            existing.Phone = warehouse.Phone;
            existing.Email = warehouse.Email;
            existing.IsActive = warehouse.IsActive;
            existing.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return true;
        }

        // ==========================================
        // DELETE
        // ==========================================
        public async Task<bool> DeleteAsync(int id)
        {
            var warehouse =
                await _context.Warehouses
                    .FirstOrDefaultAsync(
                        x => x.Id == id
                    );

            if (warehouse == null)
            {
                return false;
            }

            _context.Warehouses.Remove(warehouse);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}