using ERP.DOMAINMODELS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.APPLICATIONBUSINESS.Interfaces.IRepository
{
    public interface IPermissionRepository
    {
        Task<List<Permission>> GetAllAsync();

        Task<Permission?> GetByIdAsync(int id);

        Task<Permission> AddAsync(Permission permission);

        Task<bool> UpdateAsync(Permission permission);

        Task<bool> DeleteAsync(int id);
    }
}
