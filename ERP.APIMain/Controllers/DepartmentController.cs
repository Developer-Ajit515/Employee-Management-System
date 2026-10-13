using ERP.APPLICATIONBUSINESS.Interfaces.IServices;
using ERP.DOMAINMODELS.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ERP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        // GET: api/Department
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var departments = await _departmentService.GetAllAsync();

            return Ok(departments);
        }

        // GET: api/Department/2
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var department = await _departmentService.GetByIdAsync(id);

            if (department == null)
                return NotFound(new
                {
                    message = "Department not found."
                });

            return Ok(department);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Department department)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdDepartment =
                await _departmentService.AddAsync(department);

            return CreatedAtAction(
                nameof(GetById),
                new { id = createdDepartment.Id },
                createdDepartment
            );
        }

        // PUT: api/Department/2
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            int id,
            [FromBody] Department department)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingDepartment =
                await _departmentService.GetByIdAsync(id);

            if (existingDepartment == null)
                return NotFound(new
                {
                    message = "Department not found."
                });

            department.Id = id;

            await _departmentService.UpdateAsync(department);

            return Ok(new
            {
                message = "Department updated successfully."
            });
        }

        // DELETE: api/Department/2
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existingDepartment =
                await _departmentService.GetByIdAsync(id);

            if (existingDepartment == null)
                return NotFound(new
                {
                    message = "Department not found."
                });

            await _departmentService.DeleteAsync(id);

            return Ok(new
            {
                message = "Department deleted successfully."
            });
        }
    }
}