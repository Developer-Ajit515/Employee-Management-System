
using ERP.APPLICATIONBUSINESS.Interfaces.IServices;
using ERP.DOMAINMODELS.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ERP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class AttendanceController : ControllerBase
    {
        private readonly IAttendanceService _service;

        public AttendanceController(
            IAttendanceService service)
        {
            _service = service;
        }


        // ==========================================
        // GET ALL
        // ==========================================

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result =
                await _service.GetAllAsync();

            return Ok(result);
        }


        // ==========================================
        // GET BY ID
        // ==========================================

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(
            int id)
        {
            var result =
                await _service.GetByIdAsync(id);

            if (result == null)
            {
                return NotFound(new
                {
                    message =
                        "Attendance record not found."
                });
            }

            return Ok(result);
        }


        // ==========================================
        // CREATE
        // ==========================================

        
[HttpPost]
public async Task<IActionResult> Create([FromBody] AttendanceCreateDto dto)
        {
            try
            {
                var result = await _service.CreateAsync(dto);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
        }




        // ==========================================
        // UPDATE
        // ==========================================

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            int id,
            [FromBody] AttendanceUpdateDto dto)
        {
            try
            {
                var result =
                    await _service.UpdateAsync(
                        id,
                        dto);

                if (result == null)
                {
                    return NotFound(new
                    {
                        message =
                            "Attendance record not found."
                    });
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
        }


        // ==========================================
        // DELETE
        // ==========================================

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(
            int id)
        {
            var deleted =
                await _service.DeleteAsync(id);

            if (!deleted)
            {
                return NotFound(new
                {
                    message =
                        "Attendance record not found."
                });
            }

            return Ok(new
            {
                message =
                    "Attendance deleted successfully."
            });
        }
    }
}

