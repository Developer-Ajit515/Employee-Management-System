using ERP.APPLICATIONBUSINESS.Interfaces.IServices;
using ERP.DOMAINMODELS.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ERP.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize]
    public class SalesOrderController : ControllerBase
    {
        private readonly ISalesOrderService _service;

        public SalesOrderController(
            ISalesOrderService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var orders = await _service.GetAllAsync();

            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var order =
                await _service.GetByIdAsync(id);

            if (order == null)
            {
                return NotFound(new
                {
                    message = "Sales order not found"
                });
            }

            return Ok(order);
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] CreateSalesOrderDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (dto.Items == null ||
                dto.Items.Count == 0)
            {
                return BadRequest(new
                {
                    message =
                        "Sales order must contain at least one item."
                });
            }

            var order =
                await _service.CreateAsync(dto);

            return CreatedAtAction(
                nameof(GetById),
                new { id = order.Id },
                order);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            int id,
            [FromBody] CreateSalesOrderDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (dto.Items == null ||
                dto.Items.Count == 0)
            {
                return BadRequest(new
                {
                    message =
                        "Sales order must contain at least one item."
                });
            }

            var order =
                await _service.UpdateAsync(id, dto);

            if (order == null)
            {
                return NotFound(new
                {
                    message = "Sales order not found"
                });
            }

            return Ok(order);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result =
                await _service.DeleteAsync(id);

            if (!result)
            {
                return NotFound(new
                {
                    message = "Sales order not found"
                });
            }

            return Ok(new
            {
                message =
                    "Sales order deleted successfully"
            });
        }
    }
}