using ERP.APPLICATIONBUSINESS.Interfaces.IRepository;
using ERP.DOMAINMODELS.Dtos;
using ERP.INFRASTRUCTUREDATA.Data;
using Microsoft.EntityFrameworkCore;

namespace ERP.INFRASTRUCTUREDATA.Repositories
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly AppDbContext _context;

        public DashboardRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<DashboardDto>
     GetDashboardDataAsync()
        {
            var totalUsers =
                await _context.Users.CountAsync();

            var totalEmployees =
                await _context.Employees.CountAsync();

            var totalProjects =
                await _context.Projects.CountAsync();

            var totalTasks =
                await _context.ProjectTasks.CountAsync();


            var completedTasks =
                await _context.ProjectTasks
                    .CountAsync(x => x.IsCompleted);


            var pendingTasks =
                await _context.ProjectTasks
                    .CountAsync(x => !x.IsCompleted);


            var activeUsers =
                await _context.Users
                    .CountAsync(x => x.IsActive);


            var activeEmployees =
                await _context.Employees
                    .CountAsync(x => x.Status);


            var activeProjects =
                await _context.Projects
                    .CountAsync(x => x.IsActive);


            var departments =
    await _context.Employees
        .Include(x => x.Department)
        .GroupBy(x => x.Department != null
            ? x.Department.Name
            : "Unassigned")
        .Select(x => new EmployeeDepartmentDto
        {
            Department = x.Key,
            Count = x.Count()
        })
        .OrderByDescending(x => x.Count)
        .ToListAsync();


            var projects =
                await _context.Projects
                    .Select(project => new RecentProjectDto
                    {
                        Id = project.Id,

                        Name = project.Name,

                        IsActive =
                            project.IsActive,

                        TaskCount =
                            project.Tasks.Count(),

                        CompletedTasks =
                            project.Tasks
                                .Count(x => x.IsCompleted)
                    })
                    .OrderByDescending(x => x.Id)
                    .Take(5)
                    .ToListAsync();

            var recentActivities =
    await _context.AuditLogs
        .OrderByDescending(x => x.CreatedAt)
        .Take(10)
        .Select(x => new RecentActivityDto
        {
            Type = x.EntityName,
            Title = x.Action,
            Description = x.Description,
            CreatedAt = x.CreatedAt
        })
        .ToListAsync();

            var recentEmployees =
    await _context.Employees
        .Include(x => x.Department)
        .OrderByDescending(x => x.JoiningDate)
        .Take(5)
        .Select(x => new RecentEmployeeDto
        {
            EmployeeId = x.EmployeeId,

            Name = x.FirstName + " " + x.LastName,

            Department = x.Department != null
                ? x.Department.Name
                : "Unassigned",

            Designation = x.Designation,

            JoiningDate = x.JoiningDate,

            Status = x.Status
        })
        .ToListAsync();

            return new DashboardDto
            {
                TotalUsers = totalUsers,

                TotalEmployees = totalEmployees,

                TotalProjects = totalProjects,

                TotalTasks = totalTasks,

                CompletedTasks = completedTasks,

                PendingTasks = pendingTasks,

                ActiveUsers = activeUsers,

                ActiveEmployees = activeEmployees,

                ActiveProjects = activeProjects,


                // ============================
                // PROJECT ANALYTICS
                // ============================

                ProjectAnalytics = new ProjectAnalyticsDto
                {
                    Total = totalProjects,

                    Active = activeProjects,

                    Inactive = totalProjects - activeProjects
                },


                // ============================
                // TASK ANALYTICS
                // ============================

                TaskAnalytics = new TaskAnalyticsDto
                {
                    Total = totalTasks,

                    Completed = completedTasks,

                    Pending = pendingTasks
                },


                // ============================
                // DEPARTMENTS
                // ============================

                EmployeeDepartments = departments,


                // ============================
                // RECENT PROJECTS
                // ============================

                RecentProjects = projects,


                // ============================
                // RECENT ACTIVITIES
                // ============================

                RecentActivities = recentActivities,


                // ============================
                // RECENT EMPLOYEES
                // ============================

                RecentEmployees = recentEmployees
            };
        }
    }
}