using ERP.APPLICATIONBUSINESS.Interfaces.IRepository;
using ERP.DOMAINMODELS.Entities;
using ERP.INFRASTRUCTUREDATA.Data;
using Microsoft.EntityFrameworkCore;

namespace ERP.INFRASTRUCTUREDATA.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        // =====================================================
        // GET USER BY EMAIL
        // =====================================================
        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users
     .Include(u => u.UserRoles)
         .ThenInclude(ur => ur.Role)
             .ThenInclude(r => r.RolePermissions)
                 .ThenInclude(rp => rp.Permission)
     .FirstOrDefaultAsync(u => u.Email == email);
        }

        // =====================================================
        // GET ALL USERS
        // =====================================================
        public async Task<List<User>> GetUsersAsync()
        {
            return await _context.Users
                .Include(u => u.UserRoles)
                    .ThenInclude(ur => ur.Role)
                        .ThenInclude(r => r.RolePermissions)
                            .ThenInclude(rp => rp.Permission)
                .ToListAsync();
        }

        // =====================================================
        // GET USER BY ID
        // =====================================================
        public async Task<User?> GetByIdAsync(int id)
        {
            return await _context.Users
                .Include(u => u.UserRoles)
                    .ThenInclude(ur => ur.Role)
                        .ThenInclude(r => r.RolePermissions)
                            .ThenInclude(rp => rp.Permission)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        // =====================================================
        // ADD USER
        // =====================================================
        public async Task<bool> AddUserAsync(User user)
        {
            await _context.Users.AddAsync(user);

            await _context.SaveChangesAsync();

            return true;
        }

        // =====================================================
        // UPDATE USER
        // =====================================================
        public async Task<bool> UpdateUserAsync(User user)
        {
            var existingUser = await _context.Users
                .FirstOrDefaultAsync(x => x.Id == user.Id);

            if (existingUser == null)
            {
                return false;
            }

            existingUser.Name = user.Name;
            existingUser.Email = user.Email;
            existingUser.IsActive = user.IsActive;

            await _context.SaveChangesAsync();

            return true;
        }

        // =====================================================
        // DELETE USER
        // =====================================================
        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.Id == id);

            if (user == null)
            {
                return false;
            }

            _context.Users.Remove(user);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}