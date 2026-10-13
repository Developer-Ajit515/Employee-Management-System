using ERP.APPLICATIONBUSINESS.Interfaces.IRepository;
using ERP.APPLICATIONBUSINESS.Interfaces.IServices;
using ERP.DOMAINMODELS.Dtos;
using ERP.DOMAINMODELS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.APPLICATIONBUSINESS.Services
{
    public class PermissionService
        : IPermissionService
    {
        private readonly IPermissionRepository
            _repository;

        public PermissionService(
            IPermissionRepository repository
        )
        {
            _repository = repository;
        }

        public async Task<List<PermissionDto>>
            GetAllAsync()
        {
            var permissions =
                await _repository.GetAllAsync();

            return permissions.Select(x =>
                new PermissionDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description
                }).ToList();
        }

        public async Task<PermissionDto?>
            GetByIdAsync(int id)
        {
            var permission =
                await _repository.GetByIdAsync(id);

            if (permission == null)
            {
                return null;
            }

            return new PermissionDto
            {
                Id = permission.Id,
                Name = permission.Name,
                Description =
                    permission.Description
            };
        }

        public async Task<PermissionDto>
            AddAsync(CreatePermissionDto dto)
        {
            var permission = new Permission
            {
                Name = dto.Name,
                Description = dto.Description
            };

            var created =
                await _repository.AddAsync(permission);

            return new PermissionDto
            {
                Id = created.Id,
                Name = created.Name,
                Description = created.Description
            };
        }

        public async Task<bool>
            UpdateAsync(UpdatePermissionDto dto)
        {
            var permission = new Permission
            {
                Id = dto.Id,
                Name = dto.Name,
                Description = dto.Description
            };

            return await _repository
                .UpdateAsync(permission);
        }

        public async Task<bool>
            DeleteAsync(int id)
        {
            return await _repository
                .DeleteAsync(id);
        }
    }
}
