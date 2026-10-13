using ERP.APPLICATIONBUSINESS.Interfaces.IRepository;
using ERP.APPLICATIONBUSINESS.Interfaces.IServices;
using ERP.DOMAINMODELS.DTOs;
using ERP.DOMAINMODELS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.APPLICATIONBUSINESS.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        // GET ALL
        public async Task<IEnumerable<EmployeeDto>> GetAllAsync()
        {
            var employees = await _employeeRepository.GetAllAsync();

            return employees.Select(MapToDto);
        }

        // GET BY ID
        public async Task<EmployeeDto?> GetByIdAsync(int id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);

            if (employee == null)
                return null;

            return MapToDto(employee);
        }

        // ADD
        public async Task<EmployeeDto> AddAsync(EmployeeRequestDto dto)
        {
            var employee = new Employee
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Phone = dto.Phone,
                DepartmentId = dto.DepartmentId,
                Designation = dto.Designation,
                Salary = dto.Salary,
                JoiningDate = dto.JoiningDate,
                Status = dto.Status
            };

            var createdEmployee =
                await _employeeRepository.AddAsync(employee);

            // Get again so DepartmentName is populated
            var employeeWithDepartment =
                await _employeeRepository.GetByIdAsync(
                    createdEmployee.EmployeeId);

            return MapToDto(employeeWithDepartment!);
        }

        // UPDATE
        public async Task<bool> UpdateAsync(
            int id,
            EmployeeRequestDto dto)
        {
            var employee = new Employee
            {
                EmployeeId = id,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Phone = dto.Phone,
                DepartmentId = dto.DepartmentId,
                Designation = dto.Designation,
                Salary = dto.Salary,
                JoiningDate = dto.JoiningDate,
                Status = dto.Status
            };

            return await _employeeRepository.UpdateAsync(employee);
        }

        // DELETE
        public async Task<bool> DeleteAsync(int id)
        {
            return await _employeeRepository.DeleteAsync(id);
        }

        // MAPPING
        private static EmployeeDto MapToDto(Employee employee)
        {
            return new EmployeeDto
            {
                EmployeeId = employee.EmployeeId,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Email = employee.Email,
                Phone = employee.Phone,
                DepartmentId = employee.DepartmentId,
                DepartmentName = employee.Department?.Name ?? string.Empty,
                Designation = employee.Designation,
                Salary = employee.Salary,
                JoiningDate = employee.JoiningDate,
                Status = employee.Status
            };
        }
    }
}
