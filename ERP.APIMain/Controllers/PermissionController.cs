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
    public class PermissionController
        : ControllerBase
    {
        private readonly IPermissionService
            _service;

        public PermissionController(
            IPermissionService service
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

        [HttpGet("{id}")]
        public async Task<IActionResult>
            GetById(int id)
        {
            var data =
                await _service.GetByIdAsync(id);

            if (data == null)
            {
                return NotFound();
            }

            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult>
            Create(
                CreatePermissionDto dto
            )
        {
            var created =
                await _service.AddAsync(dto);

            return Ok(created);
        }

        [HttpPut]
        public async Task<IActionResult>
            Update(
                UpdatePermissionDto dto
            )
        {
            var updated =
                await _service.UpdateAsync(dto);

            if (!updated)
            {
                return NotFound();
            }

            return Ok(
                new
                {
                    message =
                        "Permission updated successfully"
                });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult>
            Delete(int id)
        {
            var deleted =
                await _service.DeleteAsync(id);

            if (!deleted)
            {
                return NotFound();
            }

            return Ok(
                new
                {
                    message =
                        "Permission deleted successfully"
                });
        }
    }
}
