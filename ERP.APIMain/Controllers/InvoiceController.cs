
using ERP.APPLICATIONBUSINESS.Interfaces;
using ERP.DOMAINMODELS.Dtos.ERP.APPLICATIONBUSINESS.DTOs.Invoice;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ERP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceService _service;

        public InvoiceController(IInvoiceService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var invoices = await _service.GetAllAsync();

            return Ok(invoices);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var invoice = await _service.GetByIdAsync(id);

            if (invoice == null)
                return NotFound(new
                {
                    message = "Invoice not found"
                });

            return Ok(invoice);
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] CreateInvoiceDto dto)
        {
            if (dto.InvoiceItems == null ||
                dto.InvoiceItems.Count == 0)
            {
                return BadRequest(new
                {
                    message =
                        "Invoice must contain at least one item."
                });
            }

            var invoice =
                await _service.CreateAsync(dto);

            return CreatedAtAction(
                nameof(GetById),
                new { id = invoice.InvoiceId },
                invoice);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            int id,
            [FromBody] CreateInvoiceDto dto)
        {
            var invoice =
                await _service.UpdateAsync(id, dto);

            if (invoice == null)
                return NotFound(new
                {
                    message = "Invoice not found"
                });

            return Ok(invoice);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result =
                await _service.DeleteAsync(id);

            if (!result)
                return NotFound(new
                {
                    message = "Invoice not found"
                });

            return Ok(new
            {
                message = "Invoice deleted successfully"
            });
        }
    }
}