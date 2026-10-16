using ERP.APPLICATIONBUSINESS.Interfaces.IRepository;
using ERP.APPLICATIONBUSINESS.Interfaces.IServices;
using ERP.DOMAINMODELS.Dtos.Notifications;
using ERP.DOMAINMODELS.Entities;

namespace ERP.APPLICATIONBUSINESS.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly INotificationService _notificationService;

        public TaskService(
            ITaskRepository taskRepository,
            IEmployeeRepository employeeRepository,
            INotificationService notificationService)
        {
            _taskRepository = taskRepository;
            _employeeRepository = employeeRepository;
            _notificationService = notificationService;
        }

        public async Task<List<ProjectTask>> GetTasksAsync()
        {
            return await _taskRepository.GetTasksAsync();
        }

        public async Task<ProjectTask?> GetByIdAsync(int id)
        {
            return await _taskRepository.GetByIdAsync(id);
        }

        public async Task<ProjectTask?> AddTaskAsync(ProjectTask task)
        {
            // Generate TaskCode if it is missing
            if (string.IsNullOrWhiteSpace(task.TaskCode))
            {
                task.TaskCode = $"TASK-{Guid.NewGuid():N}"
                    .Substring(0, 13)
                    .ToUpperInvariant();
            }

            // Validate employee-to-user mapping before saving
            int? userId = null;

            if (task.AssignedToEmployeeId.HasValue)
            {
                userId = await _employeeRepository
                    .GetUserIdByEmployeeIdAsync(
                        task.AssignedToEmployeeId.Value);

                if (!userId.HasValue)
                {
                    throw new InvalidOperationException(
                        "The selected employee is not linked to a user account.");
                }
            }

            // Save task
            var savedTask = await _taskRepository.AddTaskAsync(task);

            if (savedTask == null)
            {
                return null;
            }

            // Notify assigned employee
            if (userId.HasValue)
            {
                await _notificationService.CreateAsync(
                    new CreateNotificationDto
                    {
                        UserId = userId.Value,
                        Title = "New Task Assigned",
                        Message = $"You have been assigned a new task: {savedTask.Title}",
                        Type = "Info",
                        ReferenceType = "Task",
                        ReferenceId = savedTask.Id
                    });
            }

            return savedTask;
        }

        public async Task<bool> UpdateTaskAsync(ProjectTask task)
        {
            return await _taskRepository.UpdateTaskAsync(task);
        }

        public async Task<bool> DeleteTaskAsync(int id)
        {
            return await _taskRepository.DeleteTaskAsync(id);
        }
    }
}