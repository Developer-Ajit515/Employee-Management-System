using ERP.APPLICATIONBUSINESS.Interfaces.IServices;
using ERP.DOMAINMODELS.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ERP.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize]
    public class TimesheetController : ControllerBase
    {
        private readonly ITimesheetService _service;

        public TimesheetController(
            ITimesheetService service)
        {
            _service = service;
        }

        // GET: api/Timesheet
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result =
                await _service.GetAllAsync();

            return Ok(result);
        }

        // GET: api/Timesheet/1
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result =
                await _service.GetByIdAsync(id);

            if (result == null)
                return NotFound(new
                {
                    message = "Timesheet not found."
                });

            return Ok(result);
        }

        // POST: api/Timesheet
        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] TimesheetCreateDto dto)
        {
            try
            {
                var result =
                    await _service.CreateAsync(dto);

                return CreatedAtAction(
                    nameof(GetById),
                    new { id = result.Id },
                    result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
        }

        // PUT: api/Timesheet/1
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            int id,
            [FromBody] TimesheetUpdateDto dto)
        {
            try
            {
                var result =
                    await _service.UpdateAsync(id, dto);

                if (result == null)
                    return NotFound(new
                    {
                        message = "Timesheet not found."
                    });

                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
        }

        // DELETE: api/Timesheet/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted =
                await _service.DeleteAsync(id);

            if (!deleted)
                return NotFound(new
                {
                    message = "Timesheet not found."
                });

            return Ok(new
            {
                message = "Timesheet deleted successfully."
            });
        }
    }
}