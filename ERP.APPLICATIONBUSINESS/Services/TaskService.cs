using ERP.APPLICATIONBUSINESS.Interfaces.IRepository;
using ERP.APPLICATIONBUSINESS.Interfaces.IServices;
using ERP.DOMAINMODELS.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.APPLICATIONBUSINESS.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;

        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<List<ProjectTask>> GetTasksAsync()
        {
            return await _taskRepository.GetTasksAsync();
        }

        public async Task<ProjectTask?> GetByIdAsync(int id)
        {
            return await _taskRepository.GetByIdAsync(id);
        }

        public async Task<bool> AddTaskAsync(ProjectTask task)
        {
            return await _taskRepository.AddTaskAsync(task);
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