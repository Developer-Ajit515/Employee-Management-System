using ERP.DOMAINMODELS.Entities;

namespace ERP.APPLICATIONBUSINESS.Interfaces.IRepository
{
    public interface IProjectRepository
    {
        Task<List<Project>> GetProjectsAsync();

        Task<Project?> GetByIdAsync(int id);

        Task<bool> AddProjectAsync(Project project);

        Task<bool> UpdateProjectAsync(Project project);

        Task<bool> DeleteProjectAsync(int id);
    }
}