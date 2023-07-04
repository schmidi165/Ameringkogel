using UserManagement.Models.DTOs;

namespace UserManagement.Services
{
    public interface IUserGroupService
    {
        Task<IEnumerable<UserGroupDTO>> GetAllUserGroups();
    }
}