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
    public class PermissionRepository
        : IPermissionRepository
    {
        private readonly AppDbContext _context;

        public PermissionRepository(
            AppDbContext context
        )
        {
            _context = context;
        }

        public async Task<List<Permission>>
            GetAllAsync()
        {
            return await _context.Permissions
                .AsNoTracking()
                .OrderBy(x => x.Name)
                .ToListAsync();
        }

        public async Task<Permission?>
            GetByIdAsync(int id)
        {
            return await _context.Permissions
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Permission>
            AddAsync(Permission permission)
        {
            await _context.Permissions
                .AddAsync(permission);

            await _context.SaveChangesAsync();

            return permission;
        }

        public async Task<bool>
            UpdateAsync(Permission permission)
        {
            var existing =
                await _context.Permissions
                .FirstOrDefaultAsync(x =>
                    x.Id == permission.Id);

            if (existing == null)
            {
                return false;
            }

            existing.Name = permission.Name;
            existing.Description =
                permission.Description;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool>
            DeleteAsync(int id)
        {
            var permission =
                await _context.Permissions
                .FirstOrDefaultAsync(x =>
                    x.Id == id);

            if (permission == null)
            {
                return false;
            }

            _context.Permissions.Remove(permission);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
