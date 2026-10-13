using ERP.APPLICATIONBUSINESS.Interfaces.IServices;
using ERP.DOMAINMODELS.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ERP.APIMain.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RolePermissionController
        : ControllerBase
    {
        private readonly IRolePermissionService
            _service;

        public RolePermissionController(
            IRolePermissionService service
        )
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult>
            GetAll()
        {
            var data =
                await _service.GetAllAsync();

            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult>
            Assign(
                AssignRolePermissionDto dto
            )
        {
            var result =
                await _service.AssignAsync(dto);

            if (result == null)
            {
                return BadRequest(
                    new
                    {
                        message =
                            "Permission already assigned"
                    });
            }

            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult>
            Remove(
                int roleId,
                int permissionId
            )
        {
            var deleted =
                await _service.RemoveAsync(
                    roleId,
                    permissionId
                );

            if (!deleted)
            {
                return NotFound();
            }

            return Ok(
                new
                {
                    message =
                        "Permission removed successfully"
                });
        }
    }
}
