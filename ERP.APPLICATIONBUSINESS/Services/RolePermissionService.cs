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
    public class RolePermissionService
        : IRolePermissionService
    {
        private readonly IRolePermissionRepository
            _repository;

        public RolePermissionService(
            IRolePermissionRepository repository
        )
        {
            _repository = repository;
        }

        public async Task<List<RolePermissionDto>>
            GetAllAsync()
        {
            var data =
                await _repository.GetAllAsync();

            return data.Select(x =>
                new RolePermissionDto
                {
                    RoleId = x.RoleId,
                    RoleName = x.Role.Name,
                    PermissionId = x.PermissionId,
                    PermissionName =
                        x.Permission.Name
                }).ToList();
        }

        public async Task<RolePermissionDto?>
            AssignAsync(
                AssignRolePermissionDto dto
            )
        {
            var exists =
                await _repository.GetAsync(
                    dto.RoleId,
                    dto.PermissionId
                );

            if (exists != null)
            {
                return null;
            }

            var entity =
                new RolePermission
                {
                    RoleId = dto.RoleId,
                    PermissionId =
                        dto.PermissionId
                };

            await _repository.AddAsync(entity);

            return new RolePermissionDto
            {
                RoleId = dto.RoleId,
                PermissionId =
                    dto.PermissionId
            };
        }

        public async Task<bool>
            RemoveAsync(
                int roleId,
                int permissionId
            )
        {
            return await _repository
                .DeleteAsync(
                    roleId,
                    permissionId
                );
        }
    }
}
