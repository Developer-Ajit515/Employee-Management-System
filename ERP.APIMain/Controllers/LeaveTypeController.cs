using ERP.APPLICATIONBUSINESS.Interfaces.IServices;
using ERP.DOMAINMODELS.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ERP.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize]
    public class LeaveTypeController : ControllerBase
    {
        private readonly ILeaveTypeService _service;

        public LeaveTypeController(ILeaveTypeService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);

            if (result == null)
                return NotFound(new
                {
                    message = "Leave type not found"
                });

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateLeaveTypeDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                return BadRequest(new
                {
                    message = "Leave type name is required"
                });

            if (dto.DefaultDays < 0)
                return BadRequest(new
                {
                    message = "Default days cannot be negative"
                });

            var result = await _service.CreateAsync(dto);

            return CreatedAtAction(
                nameof(GetById),
                new { id = result.LeaveTypeId },
                result
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            int id,
            CreateLeaveTypeDto dto)
        {
            var result = await _service.UpdateAsync(id, dto);

            if (!result)
                return NotFound(new
                {
                    message = "Leave type not found"
                });

            return Ok(new
            {
                message = "Leave type updated successfully"
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteAsync(id);

            if (!result)
                return NotFound(new
                {
                    message = "Leave type not found"
                });

            return Ok(new
            {
                message = "Leave type deleted successfully"
            });
        }
    }
}