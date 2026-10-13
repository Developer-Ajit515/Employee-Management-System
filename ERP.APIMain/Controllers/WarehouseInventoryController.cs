
using ERP.APPLICATIONBUSINESS.Interfaces.IServices;
using ERP.DOMAINMODELS.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ERP.APIMain.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class WarehouseInventoryController : ControllerBase
    {
        private readonly IWarehouseInventoryService _service;

        public WarehouseInventoryController(
            IWarehouseInventoryService service)
        {
            _service = service;
        }

        // ==========================================
        // GET ALL
        // GET: api/WarehouseInventory
        // ==========================================
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data =
                await _service.GetAllAsync();

            return Ok(data);
        }

        // ==========================================
        // GET BY WAREHOUSE
        // GET: api/WarehouseInventory/warehouse/1
        // ==========================================
        [HttpGet("warehouse/{warehouseId}")]
        public async Task<IActionResult>
            GetByWarehouseId(int warehouseId)
        {
            var data =
                await _service
                    .GetByWarehouseIdAsync(warehouseId);

            return Ok(data);
        }

        // ==========================================
        // GET BY ID
        // GET: api/WarehouseInventory/1
        // ==========================================
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

        // ==========================================
        // CREATE
        // POST: api/WarehouseInventory
        // ==========================================
        [HttpPost]
        public async Task<IActionResult>
            Create(CreateWarehouseInventoryDto dto)
        {
            try
            {
                var created =
                    await _service.AddAsync(dto);

                return Ok(created);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new
                {
                    message = ex.Message
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
        }

        // ==========================================
        // UPDATE
        // PUT: api/WarehouseInventory
        // ==========================================
        [HttpPut]
        public async Task<IActionResult>
            Update(UpdateWarehouseInventoryDto dto)
        {
            try
            {
                var updated =
                    await _service.UpdateAsync(dto);

                if (!updated)
                {
                    return NotFound();
                }

                return Ok(new
                {
                    message =
                        "Warehouse product updated successfully"
                });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new
                {
                    message = ex.Message
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
        }

        // ==========================================
        // DELETE
        // DELETE: api/WarehouseInventory/1
        // ==========================================
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

            return Ok(new
            {
                message =
                    "Product removed from warehouse successfully"
            });
        }
    }
}

