using ERP.APPLICATIONBUSINESS.Interfaces.IServices;
using ERP.DOMAINMODELS.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ERP.API.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        // GET ALL ROLES

        [HttpGet]
        public async Task<IActionResult> GetRoles()
        {
            try
            {
                var roles = await _roleService.GetAllAsync();

                return Ok(roles);
            }
            catch (Exception ex)
            {
                return StatusCode(
                    500,
                    new
                    {
                        message = ex.Message
                    }
                );
            }
        }

        // GET ROLE BY ID

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoleById(int id)
        {
            try
            {
                var role = await _roleService.GetByIdAsync(id);

                if (role == null)
                {
                    return NotFound(
                        new
                        {
                            message = "Role not found"
                        }
                    );
                }

                return Ok(role);
            }
            catch (Exception ex)
            {
                return StatusCode(
                    500,
                    new
                    {
                        message = ex.Message
                    }
                );
            }
        }

        // ADD ROLE

        [HttpPost]
        public async Task<IActionResult> AddRole(
            [FromBody] Role role
        )
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var createdRole =
                    await _roleService.AddAsync(role);

                return Ok(
                    new
                    {
                        message = "Role added successfully",
                        data = createdRole
                    }
                );
            }
            catch (Exception ex)
            {
                return StatusCode(
                    500,
                    new
                    {
                        message = ex.Message
                    }
                );
            }
        }

        // UPDATE ROLE

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRole(
            int id,
            [FromBody] Role role
        )
        {
            try
            {
                if (id != role.Id)
                {
                    return BadRequest(
                        new
                        {
                            message = "Invalid role id"
                        }
                    );
                }

                var updated =
                    await _roleService.UpdateAsync(role);

                if (!updated)
                {
                    return NotFound(
                        new
                        {
                            message = "Role not found"
                        }
                    );
                }

                return Ok(
                    new
                    {
                        message = "Role updated successfully"
                    }
                );
            }
            catch (Exception ex)
            {
                return StatusCode(
                    500,
                    new
                    {
                        message = ex.Message
                    }
                );
            }
        }

        // DELETE ROLE

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(int id)
        {
            try
            {
                var deleted =
                    await _roleService.DeleteAsync(id);

                if (!deleted)
                {
                    return NotFound(
                        new
                        {
                            message = "Role not found"
                        }
                    );
                }

                return Ok(
                    new
                    {
                        message = "Role deleted successfully"
                    }
                );
            }
            catch (Exception ex)
            {
                return StatusCode(
                    500,
                    new
                    {
                        message = ex.Message
                    }
                );
            }
        }
    }
}