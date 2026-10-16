using ERP.APPLICATIONBUSINESS.Interfaces.IRepository;
using ERP.DOMAINMODELS.Entities;
using ERP.INFRASTRUCTUREDATA.Data;
using Microsoft.EntityFrameworkCore;

namespace ERP.INFRASTRUCTUREDATA.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly AppDbContext _context;

        public TaskRepository(AppDbContext context)
        {
            _context = context;
        }

        // Get all tasks with optional project info
        public async Task<List<ProjectTask>> GetTasksAsync()
        {
            return await _context.ProjectTasks
                                 .Include(t => t.Project)
                                 .ToListAsync();
        }

        // Get task by ID
        public async Task<ProjectTask?> GetByIdAsync(int id)
        {
            return await _context.ProjectTasks
                                 .Include(t => t.Project)
                                 .FirstOrDefaultAsync(t => t.Id == id);
        }

        // Add a new task

        public async Task<ProjectTask?> AddTaskAsync(ProjectTask task)
        {
            _context.ProjectTasks.Add(task);

            var result = await _context.SaveChangesAsync();

            return result > 0 ? task : null;
        }

        // Update existing task
        public async Task<bool> UpdateTaskAsync(ProjectTask task)
        {
            _context.ProjectTasks.Update(task);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

        // Delete task by ID
        public async Task<bool> DeleteTaskAsync(int id)
        {
            var task = await _context.ProjectTasks.FindAsync(id);
            if (task == null) return false;

            _context.ProjectTasks.Remove(task);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

       
    }
}