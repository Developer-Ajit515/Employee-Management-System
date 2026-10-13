using ERP.DOMAINMODELS.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.APPLICATIONBUSINESS.Interfaces.IServices
{
    public interface IPermissionService
    {
        Task<List<PermissionDto>> GetAllAsync();

        Task<PermissionDto?> GetByIdAsync(int id);

        Task<PermissionDto>
            AddAsync(CreatePermissionDto dto);

        Task<bool>
            UpdateAsync(UpdatePermissionDto dto);

        Task<bool> DeleteAsync(int id);
    }
}
