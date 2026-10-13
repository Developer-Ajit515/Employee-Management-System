using ERP.DOMAINMODELS.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.APPLICATIONBUSINESS.Interfaces.IServices
{
    public interface IProjectService
    {
        Task<List<Project>> GetProjectsAsync();

        Task<Project?> GetByIdAsync(int id);

        Task<bool> AddProjectAsync(Project project);

        Task<bool> UpdateProjectAsync(Project project);

        Task<bool> DeleteProjectAsync(int id);
    }
}