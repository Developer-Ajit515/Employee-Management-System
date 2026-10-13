using ERP.DOMAINMODELS.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.APPLICATIONBUSINESS.Interfaces.IServices
{
    public interface ITaskService
    {
        Task<List<ProjectTask>> GetTasksAsync();

        Task<ProjectTask?> GetByIdAsync(int id);

        Task<bool> AddTaskAsync(ProjectTask task);

        Task<bool> UpdateTaskAsync(ProjectTask task);

        Task<bool> DeleteTaskAsync(int id);
    }
}