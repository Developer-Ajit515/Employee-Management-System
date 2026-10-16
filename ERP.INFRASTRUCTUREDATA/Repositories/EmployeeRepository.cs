using ERP.APPLICATIONBUSINESS.Interfaces.IRepository;
using ERP.DOMAINMODELS.Entities;
using ERP.INFRASTRUCTUREDATA.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.INFRASTRUCTUREDATA.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext _context;

        public EmployeeRepository(AppDbContext context)
        {
            _context = context;
        }

        // GET ALL
        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            return await _context.Employees
                .Include(e => e.Department)
                .AsNoTracking()
                .OrderByDescending(e => e.EmployeeId)
                .ToListAsync();
        }

        // GET BY ID
        public async Task<Employee?> GetByIdAsync(int id)
        {
            return await _context.Employees
                .Include(e => e.Department)
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.EmployeeId == id);
        }

        public async Task<int?> GetUserIdByEmployeeIdAsync(int employeeId)
        {
            return await _context.Employees
                .Where(e => e.EmployeeId == employeeId && e.Status)
                .Select(e => e.UserId)
                .FirstOrDefaultAsync();
        }

        // ADD
        public async Task<Employee> AddAsync(Employee employee)
        {
            await _context.Employees.AddAsync(employee);

            await _context.SaveChangesAsync();

            return employee;
        }

        // UPDATE
        public async Task<bool> UpdateAsync(Employee employee)
        {
            var existingEmployee = await _context.Employees
                .FirstOrDefaultAsync(e => e.EmployeeId == employee.EmployeeId);

            if (existingEmployee == null)
                return false;

            existingEmployee.FirstName = employee.FirstName;
            existingEmployee.LastName = employee.LastName;
            existingEmployee.Email = employee.Email;
            existingEmployee.Phone = employee.Phone;
            existingEmployee.DepartmentId = employee.DepartmentId;
            existingEmployee.Designation = employee.Designation;
            existingEmployee.Salary = employee.Salary;
            existingEmployee.JoiningDate = employee.JoiningDate;
            existingEmployee.Status = employee.Status;

            await _context.SaveChangesAsync();

            return true;
        }

        // DELETE
        public async Task<bool> DeleteAsync(int id)
        {
            var employee = await _context.Employees
                .FirstOrDefaultAsync(e => e.EmployeeId == id);

            if (employee == null)
                return false;

            _context.Employees.Remove(employee);

            await _context.SaveChangesAsync();

            return true;
        }

       
    }
}
