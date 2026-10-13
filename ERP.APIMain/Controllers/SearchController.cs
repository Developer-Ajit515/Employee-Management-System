using ERP.DOMAINMODELS.Dtos;
using ERP.INFRASTRUCTUREDATA.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ERP.APIMain.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize]
    public class SearchController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SearchController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Search([FromQuery] string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return Ok(new List<SearchResultDto>());
            }

            query = query.Trim();

            var results = new List<SearchResultDto>();

            // USERS
            var users = await _context.Users
                .Where(x =>
                    x.Name.Contains(query) ||
                    x.Email.Contains(query))
                .Select(x => new SearchResultDto
                {
                    Type = "User",
                    Id = x.Id,
                    Title = x.Name,
                    Description = x.Email
                })
                .Take(10)
                .ToListAsync();

            results.AddRange(users);

            // PROJECTS
            var projects = await _context.Projects
                .Where(x =>
                    x.Name.Contains(query) ||
                    x.Description.Contains(query))
                .Select(x => new SearchResultDto
                {
                    Type = "Project",
                    Id = x.Id,
                    Title = x.Name,
                    Description = x.Description
                })
                .Take(10)
                .ToListAsync();

            results.AddRange(projects);

            // TASKS
            var tasks = await _context.ProjectTasks
                .Where(x =>
                    x.Title.Contains(query) ||
                    x.Description.Contains(query))
                .Select(x => new SearchResultDto
                {
                    Type = "Task",
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description
                })
                .Take(10)
                .ToListAsync();

            results.AddRange(tasks);

            return Ok(results.Take(20));
        }
    }
}
