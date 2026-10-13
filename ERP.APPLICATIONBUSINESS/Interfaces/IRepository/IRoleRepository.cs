using ERP.DOMAINMODELS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.APPLICATIONBUSINESS.Interfaces.IRepository
{
    public interface IRoleRepository
    {
        Task<List<Role>> GetAllAsync();

        Task<Role?> GetByIdAsync(int id);

        Task<Role> AddAsync(Role role);

        Task<bool> UpdateAsync(Role role);

        Task<bool> DeleteAsync(int id);
    }
}
