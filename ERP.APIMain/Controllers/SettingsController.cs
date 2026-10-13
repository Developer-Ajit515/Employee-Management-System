using ERP.APPLICATIONBUSINESS.Interfaces.IServices;
using ERP.DOMAINMODELS.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ERP.API.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class SettingsController : ControllerBase
    {
        private readonly ISettingsService _service;

        public SettingsController(
            ISettingsService service)
        {
            _service = service;
        }

        // GET: api/Settings
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var settings =
                await _service.GetAsync();

            return Ok(settings);
        }

        // PUT: api/Settings
        [HttpPut]
        public async Task<IActionResult> Update(
            [FromBody] UpdateSystemSettingDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (string.IsNullOrWhiteSpace(dto.CompanyName))
            {
                return BadRequest(new
                {
                    message = "Company name is required."
                });
            }

            var settings =
                await _service.UpdateAsync(dto);

            return Ok(settings);
        }
    }
}