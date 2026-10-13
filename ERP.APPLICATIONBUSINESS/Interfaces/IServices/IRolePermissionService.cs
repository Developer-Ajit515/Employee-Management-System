using ERP.DOMAINMODELS.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.APPLICATIONBUSINESS.Interfaces.IServices
{
    public interface IRolePermissionService
    {
        Task<List<RolePermissionDto>>
            GetAllAsync();

        Task<RolePermissionDto?>
            AssignAsync(
                AssignRolePermissionDto dto
            );

        Task<bool>
            RemoveAsync(
                int roleId,
                int permissionId
            );
    }
}
