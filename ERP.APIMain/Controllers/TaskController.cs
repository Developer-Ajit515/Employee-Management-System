using ERP.APPLICATIONBUSINESS.Interfaces.IServices;
using ERP.DOMAINMODELS.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ERP.APIMain.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTasks()
        {
            var tasks = await _taskService.GetTasksAsync();
            return Ok(tasks);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTask(int id)
        {
            var task = await _taskService.GetByIdAsync(id);
            if (task == null) return NotFound();
            return Ok(task);
        }

        [HttpPost]
        public async Task<IActionResult> AddTask([FromBody] ProjectTask task)
        {
            var result = await _taskService.AddTaskAsync(task);
            if (!result) return BadRequest("Failed to add task");
            return Ok("Task added successfully");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, [FromBody] ProjectTask task)
        {
            if (id != task.Id) return BadRequest("Task ID mismatch");

            var result = await _taskService.UpdateTaskAsync(task);
            if (!result) return BadRequest("Failed to update task");

            return Ok("Task updated successfully");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var result = await _taskService.DeleteTaskAsync(id);
            if (!result) return BadRequest("Failed to delete task");
            return Ok("Task deleted successfully");
        }
    }
}