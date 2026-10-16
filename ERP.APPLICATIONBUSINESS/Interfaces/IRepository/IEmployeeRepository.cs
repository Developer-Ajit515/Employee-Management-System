using ERP.DOMAINMODELS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.APPLICATIONBUSINESS.Interfaces.IRepository
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetAllAsync();

        Task<Employee?> GetByIdAsync(int id);

        Task<int?> GetUserIdByEmployeeIdAsync(int employeeId);

        Task<Employee> AddAsync(Employee employee);

        Task<bool> UpdateAsync(Employee employee);

        Task<bool> DeleteAsync(int id);
    }
}
