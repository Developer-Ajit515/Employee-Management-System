
using ERP.APPLICATIONBUSINESS.Interfaces.IServices;
using ERP.DOMAINMODELS.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ERP.APIMain.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class PurchaseOrderController : ControllerBase
    {
        private readonly IPurchaseOrderService _purchaseOrderService;

        public PurchaseOrderController(
            IPurchaseOrderService purchaseOrderService)
        {
            _purchaseOrderService = purchaseOrderService;
        }

        // GET: api/PurchaseOrder
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var purchaseOrders =
                await _purchaseOrderService.GetAllAsync();

            return Ok(purchaseOrders);
        }

        // GET: api/PurchaseOrder/1
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new
                {
                    message = "Invalid purchase order ID."
                });
            }

            var purchaseOrder =
                await _purchaseOrderService.GetByIdAsync(id);

            if (purchaseOrder == null)
            {
                return NotFound(new
                {
                    message = "Purchase order not found."
                });
            }

            return Ok(purchaseOrder);
        }

        // POST: api/PurchaseOrder
        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] PurchaseOrder purchaseOrder)
        {
            if (purchaseOrder == null)
            {
                return BadRequest(new
                {
                    message = "Purchase order data is required."
                });
            }

            var createdPurchaseOrder =
                await _purchaseOrderService.CreateAsync(purchaseOrder);

            return Ok(createdPurchaseOrder);
        }

        // PUT: api/PurchaseOrder/1
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(
     int id,
     [FromBody] PurchaseOrder purchaseOrder)
        {
            if (id <= 0)
                return BadRequest(new { message = "Invalid purchase order ID." });

            if (purchaseOrder == null)
                return BadRequest(new { message = "Purchase order data is required." });

            try
            {
                var updated = await _purchaseOrderService.UpdateAsync(id, purchaseOrder);

                if (!updated)
                    return NotFound(new { message = "Purchase order not found." });

                return Ok(new
                {
                    message = "Purchase order updated successfully.",
                    id
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // DELETE: api/PurchaseOrder/1
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new
                {
                    message = "Invalid purchase order ID."
                });
            }

            var deleted =
                await _purchaseOrderService.DeleteAsync(id);

            if (!deleted)
            {
                return NotFound(new
                {
                    message = "Purchase order not found."
                });
            }

            return Ok(new
            {
                message = "Purchase order deleted successfully."
            });
        }
    }
}

