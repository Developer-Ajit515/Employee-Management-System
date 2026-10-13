
using ERP.APPLICATIONBUSINESS.Interfaces.IServices;
using ERP.DOMAINMODELS.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ERP.APIMain.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class GoodsReceiptsController : ControllerBase
    {
        private readonly IGoodsReceiptService _service;

        public GoodsReceiptsController(IGoodsReceiptService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var receipts = await _service.GetAllAsync();
            return Ok(receipts);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (id <= 0)
                return BadRequest(new { message = "Invalid receipt ID." });

            var receipt = await _service.GetByIdAsync(id);

            if (receipt == null)
                return NotFound(new { message = "Goods receipt not found." });

            return Ok(receipt);
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] CreateGoodsReceiptDto dto)
        {
            try
            {
                var receipt = await _service.CreateAsync(dto);

                return CreatedAtAction(
                    nameof(GetById),
                    new { id = receipt.Id },
                    receipt
                );
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(
            int id,
            [FromBody] CreateGoodsReceiptDto dto)
        {
            if (id <= 0)
                return BadRequest(new { message = "Invalid receipt ID." });

            try
            {
                var receipt = await _service.UpdateAsync(id, dto);

                if (receipt == null)
                    return NotFound(new { message = "Goods receipt not found." });

                return Ok(receipt);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
                return BadRequest(new { message = "Invalid receipt ID." });

            var deleted = await _service.DeleteAsync(id);

            if (!deleted)
                return NotFound(new { message = "Goods receipt not found." });

            return Ok(new { message = "Goods receipt deleted successfully." });
        }
    }
}