using UserManagement.Models.DTOs;

namespace UserManagement.Services
{
    public interface IWorkItemService
    {
        Task<WorkItemDTO> AddWorkItem(WorkItemDTO workItem);
        Task<WorkItemDTO> AddOrUpdateItem(WorkItemDTO workItem);
        Task<IEnumerable<WorkItemDTO>> GetAllWorkItems();
    }
}