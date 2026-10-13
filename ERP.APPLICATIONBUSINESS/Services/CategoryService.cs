using ERP.APPLICATIONBUSINESS.Interfaces.IRepository;
using ERP.APPLICATIONBUSINESS.Interfaces.IServices;
using ERP.DOMAINMODELS.Dtos;
using ERP.DOMAINMODELS.Entities;

namespace ERP.APPLICATIONBUSINESS.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;

        public CategoryService(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<CategoryDto>> GetAllAsync()
        {
            var categories = await _repository.GetAllAsync();

            return categories.Select(x => new CategoryDto
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                IsActive = x.IsActive
            });
        }

        public async Task<CategoryDto?> GetByIdAsync(int id)
        {
            var category = await _repository.GetByIdAsync(id);

            if (category == null)
                return null;

            return new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description,
                IsActive = category.IsActive
            };
        }

        public async Task<CategoryDto> CreateAsync(CreateCategoryDto dto)
        {
            var category = new Category
            {
                Name = dto.Name,
                Description = dto.Description,
                IsActive = dto.IsActive
            };

            var result = await _repository.AddAsync(category);

            return new CategoryDto
            {
                Id = result.Id,
                Name = result.Name,
                Description = result.Description,
                IsActive = result.IsActive
            };
        }

        public async Task<CategoryDto?> UpdateAsync(
            int id,
            UpdateCategoryDto dto)
        {
            var category = new Category
            {
                Id = id,
                Name = dto.Name,
                Description = dto.Description,
                IsActive = dto.IsActive
            };

            var result = await _repository.UpdateAsync(category);

            if (result == null)
                return null;

            return new CategoryDto
            {
                Id = result.Id,
                Name = result.Name,
                Description = result.Description,
                IsActive = result.IsActive
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}