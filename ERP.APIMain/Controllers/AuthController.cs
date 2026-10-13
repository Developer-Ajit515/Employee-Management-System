using ERP.APPLICATIONBUSINESS.Interfaces.IServices;
using ERP.DOMAINMODELS.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ERP.APIMain.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        // PUBLIC - No JWT required
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            var result = await _authService.RegisterAsync(dto);

            if (result == "User already exists.")
            {
                return BadRequest(result);
            }

            return Ok(new
            {
                message = result
            });
        }

        // PUBLIC - No JWT required
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var result = await _authService.LoginAsync(dto);

            if (result == null)
            {
                return Unauthorized("Invalid email or password.");
            }

            return Ok(result);
        }

        // PROTECTED
        [Authorize]
        [HttpGet("me")]
        public IActionResult Me()
        {
            var id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var name = User.FindFirstValue(ClaimTypes.Name);
            var email = User.FindFirstValue(ClaimTypes.Email);

            var roles = User
                .FindAll(ClaimTypes.Role)
                .Select(x => x.Value)
                .Distinct()
                .ToList();

            var permissions = User
                .FindAll("Permission")
                .Select(x => x.Value)
                .Distinct()
                .ToList();

            return Ok(new
            {
                id,
                name,
                email,
                roles,
                permissions
            });
        }
    }
}