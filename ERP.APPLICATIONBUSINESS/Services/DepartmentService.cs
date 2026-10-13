using ERP.APPLICATIONBUSINESS.Interfaces.IRepository;
using ERP.APPLICATIONBUSINESS.Interfaces.IServices;
using ERP.DOMAINMODELS.Entities;

namespace ERP.APPLICATIONBUSINESS.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentService(
            IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        // GET ALL
        public async Task<List<Department>> GetAllAsync()
        {
            return await _departmentRepository.GetAllAsync();
        }

        // GET BY ID
        public async Task<Department?> GetByIdAsync(int id)
        {
            return await _departmentRepository.GetByIdAsync(id);
        }

        // ADD
        public async Task<Department> AddAsync(
            Department department)
        {
            return await _departmentRepository.AddAsync(department);
        }

        // UPDATE
        public async Task<bool> UpdateAsync(
            Department department)
        {
            return await _departmentRepository.UpdateAsync(department);
        }

        // DELETE
        public async Task<bool> DeleteAsync(int id)
        {
            return await _departmentRepository.DeleteAsync(id);
        }
    }
}