using ERP.APPLICATIONBUSINESS.Interfaces.IServices;
using ERP.DOMAINMODELS;
using ERP.DOMAINMODELS.Dtos;
using ERP.DOMAINMODELS.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ERP.APIMain.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/user
        
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userService.GetUsersAsync();

            var result = users.Select(x => new UserDto
            {
                Id = x.Id,
                Name = x.Name,
                Email = x.Email,
                IsActive = x.IsActive
            });

            return Ok(result);
        }

        // GET: api/user/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _userService.GetByIdAsync(id);

            if (user == null)
            {
                return NotFound("User not found");
            }

            return Ok(user);
        }

        // POST: api/user
        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] CreateUserDto dto)
        {
            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                PasswordHash = dto.Password,
                IsActive = dto.IsActive
            };

            var result = await _userService.AddUserAsync(user);

            if (!result)
            {
                return BadRequest();
            }

            return Ok(new
            {
                message = "User added successfully"
            });
        }

        // PUT: api/user/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(
    int id,
    User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            var result =
                await _userService.UpdateUserAsync(user);

            if (!result)
            {
                return NotFound();
            }

            return Ok(
                "User Updated Successfully"
            );
        }
        // DELETE: api/user/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var result =
                await _userService.DeleteUserAsync(id);

            if (!result)
            {
                return NotFound();
            }

            return Ok(
                "User Deleted Successfully"
            );
        }
    }
}