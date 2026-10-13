using ERP.APPLICATIONBUSINESS.Interfaces.IRepository;
using ERP.DOMAINMODELS.Entities;
using ERP.INFRASTRUCTUREDATA.Data;
using Microsoft.EntityFrameworkCore;

namespace ERP.INFRASTRUCTUREDATA.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly AppDbContext _context;

        public RoleRepository(AppDbContext context)
        {
            _context = context;
        }

        // GET ALL

        public async Task<List<Role>> GetAllAsync()
        {
            return await _context.Roles
                .AsNoTracking()
                .OrderBy(x => x.Name)
                .ToListAsync();
        }

        // GET BY ID

        public async Task<Role?> GetByIdAsync(int id)
        {
            return await _context.Roles
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        // ADD

        public async Task<Role> AddAsync(Role role)
        {
            await _context.Roles.AddAsync(role);

            await _context.SaveChangesAsync();

            return role;
        }

        // UPDATE

        public async Task<bool> UpdateAsync(Role role)
        {
            var existingRole = await _context.Roles
                .FirstOrDefaultAsync(x => x.Id == role.Id);

            if (existingRole == null)
            {
                return false;
            }

            existingRole.Name = role.Name;
            existingRole.Description = role.Description;
            existingRole.IsActive = role.IsActive;

            await _context.SaveChangesAsync();

            return true;
        }

        // DELETE

        public async Task<bool> DeleteAsync(int id)
        {
            var role = await _context.Roles
                .FirstOrDefaultAsync(x => x.Id == id);

            if (role == null)
            {
                return false;
            }

            _context.Roles.Remove(role);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}