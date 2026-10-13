using ERP.APPLICATIONBUSINESS.Interfaces.IRepository;
using ERP.DOMAINMODELS.Entities;
using ERP.INFRASTRUCTUREDATA.Data;
using Microsoft.EntityFrameworkCore;

namespace ERP.INFRASTRUCTUREDATA.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly AppDbContext _context;

        public ProjectRepository(AppDbContext context)
        {
            _context = context;
        }

        // Get all projects
        public async Task<List<Project>> GetProjectsAsync()
        {
            return await _context.Projects
                                 .Include(p => p.Tasks) // include tasks
                                 .ToListAsync();
        }

        // Get project by ID
        public async Task<Project?> GetByIdAsync(int id)
        {
            return await _context.Projects
                                 .Include(p => p.Tasks)
                                 .FirstOrDefaultAsync(p => p.Id == id);
        }

        // Add new project
        public async Task<bool> AddProjectAsync(Project project)
        {
            await _context.Projects.AddAsync(project);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

        // Update existing project
        public async Task<bool> UpdateProjectAsync(Project project)
        {
            _context.Projects.Update(project);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

        // Delete project by ID
        public async Task<bool> DeleteProjectAsync(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project == null) return false;

            _context.Projects.Remove(project);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }
    }
}