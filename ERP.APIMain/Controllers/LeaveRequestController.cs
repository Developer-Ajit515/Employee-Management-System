using ERP.APPLICATIONBUSINESS.Interfaces.IServices;
using ERP.DOMAINMODELS.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ERP.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize]
    public class LeaveRequestController : ControllerBase
    {
        private readonly ILeaveRequestService _service;

        public LeaveRequestController(
            ILeaveRequestService service)
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
            var result =
                await _service.GetByIdAsync(id);

            if (result == null)
            {
                return NotFound(new
                {
                    message = "Leave request not found."
                });
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            CreateLeaveRequestDto dto)
        {
            if (dto.EmployeeId <= 0)
            {
                return BadRequest(new
                {
                    message = "Employee is required."
                });
            }

            if (dto.LeaveTypeId <= 0)
            {
                return BadRequest(new
                {
                    message = "Leave type is required."
                });
            }

            if (string.IsNullOrWhiteSpace(dto.Reason))
            {
                return BadRequest(new
                {
                    message = "Reason is required."
                });
            }

            var result =
                await _service.CreateAsync(dto);

            if (!result.Success)
            {
                return BadRequest(new
                {
                    message = result.Message
                });
            }

            return CreatedAtAction(
                nameof(GetById),
                new
                {
                    id = result.Data!.LeaveRequestId
                },
                result.Data
            );
        }

        [HttpPut("{id}/approve")]
        public async Task<IActionResult> Approve(
            int id,
            [FromQuery] int approvedBy)
        {
            if (approvedBy <= 0)
            {
                return BadRequest(new
                {
                    message = "ApprovedBy is required."
                });
            }

            var result =
                await _service.ApproveAsync(
                    id,
                    approvedBy);

            if (!result.Success)
            {
                return BadRequest(new
                {
                    message = result.Message
                });
            }

            return Ok(new
            {
                message = result.Message
            });
        }

        [HttpPut("{id}/reject")]
        public async Task<IActionResult> Reject(
            int id,
            [FromQuery] int approvedBy,
            [FromBody] string? remarks)
        {
            if (approvedBy <= 0)
            {
                return BadRequest(new
                {
                    message = "ApprovedBy is required."
                });
            }

            var result =
                await _service.RejectAsync(
                    id,
                    approvedBy,
                    remarks);

            if (!result.Success)
            {
                return BadRequest(new
                {
                    message = result.Message
                });
            }

            return Ok(new
            {
                message = result.Message
            });
        }

        [HttpPut("{id}/cancel")]
        public async Task<IActionResult> Cancel(int id)
        {
            var result =
                await _service.CancelAsync(id);

            if (!result.Success)
            {
                return BadRequest(new
                {
                    message = result.Message
                });
            }

            return Ok(new
            {
                message = result.Message
            });
        }
    }
}