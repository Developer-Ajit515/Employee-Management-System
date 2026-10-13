using ERP.DOMAINMODELS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.APPLICATIONBUSINESS.Interfaces.IRepository
{
    public interface IUserRepository
    {
        Task<List<User>> GetUsersAsync();

        Task<bool> AddUserAsync(User user);

        Task<User?> GetByEmailAsync(string email);

        Task<User?> GetByIdAsync(int id);

        Task<bool> UpdateUserAsync(User user);

        Task<bool> DeleteUserAsync(int id);
    }
}
