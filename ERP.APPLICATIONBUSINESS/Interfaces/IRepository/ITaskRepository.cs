using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ERP.DOMAINMODELS.Entities;

namespace ERP.APPLICATIONBUSINESS.Interfaces.IRepository
{
    public interface ITaskRepository
    {
        Task<List<ProjectTask>> GetTasksAsync();

        Task<ProjectTask?> GetByIdAsync(int id);

        Task<bool> AddTaskAsync(ProjectTask task);

        Task<bool> UpdateTaskAsync(ProjectTask task);

        Task<bool> DeleteTaskAsync(int id);
    }
}
