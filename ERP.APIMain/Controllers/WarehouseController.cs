using ERP.APPLICATIONBUSINESS.Interfaces.IServices;
using ERP.DOMAINMODELS.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ERP.APIMain.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class WarehouseController : ControllerBase
    {
        private readonly IWarehouseService _service;

        public WarehouseController(
            IWarehouseService service)
        {
            _service = service;
        }

        // ==========================================
        // GET ALL
        // GET: api/Warehouse
        // ==========================================
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data =
                await _service.GetAllAsync();

            return Ok(data);
        }

        // ==========================================
        // GET BY ID
        // GET: api/Warehouse/1
        // ==========================================
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(
            int id)
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
        // POST: api/Warehouse
        // ==========================================
        [HttpPost]
        public async Task<IActionResult> Create(
            CreateWarehouseDto dto)
        {
            var created =
                await _service.AddAsync(dto);

            return Ok(created);
        }

        // ==========================================
        // UPDATE
        // PUT: api/Warehouse
        // ==========================================
        [HttpPut]
        public async Task<IActionResult> Update(
            UpdateWarehouseDto dto)
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
                    "Warehouse updated successfully"
            });
        }

        // ==========================================
        // DELETE
        // DELETE: api/Warehouse/1
        // ==========================================
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(
            int id)
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
                    "Warehouse deleted successfully"
            });
        }
    }
}