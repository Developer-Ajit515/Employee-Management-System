using ERP.APPLICATIONBUSINESS.Interfaces.IRepository;
using ERP.DOMAINMODELS.Entities;
using ERP.INFRASTRUCTUREDATA.Data;
using Microsoft.EntityFrameworkCore;

namespace ERP.INFRASTRUCTUREDATA.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly AppDbContext _context;

        public DepartmentRepository(AppDbContext context)
        {
            _context = context;
        }

        // GET ALL
        public async Task<List<Department>> GetAllAsync()
        {
            return await _context.Departments
                .Include(d => d.Employees)
                .AsNoTracking()
                .ToListAsync();
        }

        // GET BY ID
        public async Task<Department?> GetByIdAsync(int id)
        {
            return await _context.Departments
                .Include(d => d.Employees)
                .AsNoTracking()
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        // ADD
        public async Task<Department> AddAsync(Department department)
        {
            await _context.Departments.AddAsync(department);
            await _context.SaveChangesAsync();

            return department;
        }

        // UPDATE
        public async Task<bool> UpdateAsync(Department department)
        {
            var existingDepartment = await _context.Departments
                .FirstOrDefaultAsync(d => d.Id == department.Id);

            if (existingDepartment == null)
                return false;

            existingDepartment.Name = department.Name;
            existingDepartment.Description = department.Description;
            existingDepartment.IsActive = department.IsActive;

            await _context.SaveChangesAsync();

            return true;
        }

        // DELETE
        public async Task<bool> DeleteAsync(int id)
        {
            var department = await _context.Departments
                .FirstOrDefaultAsync(d => d.Id == id);

            if (department == null)
                return false;

            _context.Departments.Remove(department);

            await _context.SaveChangesAsync();

            return true;
        }

        public Task<Department> CreateAsync(Department department)
        {
            throw new NotImplementedException();
        }
    }
}