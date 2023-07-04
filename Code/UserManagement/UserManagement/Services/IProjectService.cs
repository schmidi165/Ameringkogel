using UserManagement.Models.DTOs;

namespace UserManagement.Services
{
    public interface IProjectService
    {
        Task<IEnumerable<ProjectDTO>> GetAllProjects();
        Task<ProjectDTO?> GetProjectById(int projectId);
    }
}