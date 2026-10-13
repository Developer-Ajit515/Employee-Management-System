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
    public class RolePermissionRepository
       : IRolePermissionRepository
    {
        private readonly AppDbContext _context;

        public RolePermissionRepository(
            AppDbContext context
        )
        {
            _context = context;
        }

        public async Task<List<RolePermission>>
            GetAllAsync()
        {
            return await _context.RolePermissions
                .Include(x => x.Role)
                .Include(x => x.Permission)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<RolePermission?>
            GetAsync(
                int roleId,
                int permissionId
            )
        {
            return await _context.RolePermissions
                .FirstOrDefaultAsync(x =>
                    x.RoleId == roleId &&
                    x.PermissionId == permissionId);
        }

        public async Task<RolePermission>
            AddAsync(
                RolePermission rolePermission
            )
        {
            await _context.RolePermissions
                .AddAsync(rolePermission);

            await _context.SaveChangesAsync();

            return rolePermission;
        }

        public async Task<bool>
            DeleteAsync(
                int roleId,
                int permissionId
            )
        {
            var entity =
                await _context.RolePermissions
                .FirstOrDefaultAsync(x =>
                    x.RoleId == roleId &&
                    x.PermissionId == permissionId);

            if (entity == null)
            {
                return false;
            }

            _context.RolePermissions
                .Remove(entity);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
