using ERP.DOMAINMODELS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.APPLICATIONBUSINESS.Interfaces.IServices
{
    public interface IUserService
    {
        Task<List<User>> GetUsersAsync();

        Task<User?> GetByIdAsync(int id);

        Task<bool> AddUserAsync(User user);

        Task<bool> UpdateUserAsync(User user);

        Task<bool> DeleteUserAsync(int id);
    }
}
