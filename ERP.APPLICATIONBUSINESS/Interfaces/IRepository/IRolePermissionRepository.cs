using ERP.DOMAINMODELS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.APPLICATIONBUSINESS.Interfaces.IRepository
{
    public interface IRolePermissionRepository
    {
        Task<List<RolePermission>>
            GetAllAsync();

        Task<RolePermission?>
            GetAsync(
                int roleId,
                int permissionId
            );

        Task<RolePermission>
            AddAsync(
                RolePermission rolePermission
            );

        Task<bool>
            DeleteAsync(
                int roleId,
                int permissionId
            );
    }
}
