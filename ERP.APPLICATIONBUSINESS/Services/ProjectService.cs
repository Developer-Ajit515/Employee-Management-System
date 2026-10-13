using ERP.APPLICATIONBUSINESS.Interfaces.IRepository;
using ERP.APPLICATIONBUSINESS.Interfaces.IServices;
using ERP.DOMAINMODELS.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.APPLICATIONBUSINESS.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectService(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<List<Project>> GetProjectsAsync()
        {
            return await _projectRepository.GetProjectsAsync();
        }

        public async Task<Project?> GetByIdAsync(int id)
        {
            return await _projectRepository.GetByIdAsync(id);
        }

        public async Task<bool> AddProjectAsync(Project project)
        {
            return await _projectRepository.AddProjectAsync(project);
        }

        public async Task<bool> UpdateProjectAsync(Project project)
        {
            return await _projectRepository.UpdateProjectAsync(project);
        }

        public async Task<bool> DeleteProjectAsync(int id)
        {
            return await _projectRepository.DeleteProjectAsync(id);
        }
    }
}