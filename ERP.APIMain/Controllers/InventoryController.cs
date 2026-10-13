using ERP.APPLICATIONBUSINESS.Interfaces.IServices;
using ERP.DOMAINMODELS.Dtos;
using ERP.DOMAINMODELS.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ERP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryService _service;

        public InventoryController(IInventoryService service)
        {
            _service = service;
        }

        // GET: api/Inventory
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var inventory = await _service.GetAllAsync();

            return Ok(inventory);
        }

        // GET: api/Inventory/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var inventory = await _service.GetByIdAsync(id);

            if (inventory == null)
            {
                return NotFound(new
                {
                    message = "Inventory record not found."
                });
            }

            return Ok(inventory);
        }

        // GET: api/Inventory/product/5
        [HttpGet("product/{productId}")]
        public async Task<IActionResult> GetByProductId(int productId)
        {
            var inventory =
                await _service.GetByProductIdAsync(productId);

            return Ok(inventory);
        }

        // POST: api/Inventory/stock-in
        [HttpPost("stock-in")]
        public async Task<IActionResult> StockIn(
      [FromBody] CreateStockInDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result = await _service.StockInAsync(dto);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    message = ex.Message,
                    innerException = ex.InnerException?.Message
                });
            }
        }

        // POST: api/Inventory/stock-out
        [HttpPost("stock-out")]
        public async Task<IActionResult> StockOut(
            [FromBody] CreateStockOutDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result = await _service.StockOutAsync(dto);

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

        // POST: api/Inventory/adjustment
        [HttpPost("adjustment")]
        public async Task<IActionResult> StockAdjustment(
            [FromBody] CreateStockAdjustmentDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result =
                    await _service.StockAdjustmentAsync(dto);

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

        // DELETE: api/Inventory/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);

            if (!deleted)
            {
                return NotFound(new
                {
                    message = "Inventory record not found."
                });
            }

            return Ok(new
            {
                message = "Inventory record deleted successfully."
            });
        }
    }
}