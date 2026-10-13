using ERP.DOMAINMODELS.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.APPLICATIONBUSINESS.Interfaces.IServices
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeDto>> GetAllAsync();

        Task<EmployeeDto?> GetByIdAsync(int id);

        Task<EmployeeDto> AddAsync(EmployeeRequestDto dto);

        Task<bool> UpdateAsync(int id, EmployeeRequestDto dto);

        Task<bool> DeleteAsync(int id);
    }
}
