using UserManagement.Models.DTOs;

namespace UserManagement.Services
{
    public interface IUserService
    {
        Task<IEnumerable<UserDTO>> GetAllUsers();
        Task<IEnumerable<UserDTO>> GetAllUsersWithinWorkGroup(int groupId);
        Task<UserDTO> GetUserById(int userId);
        Task<UserDTO> UpdateUser(UserDTO dto);
    }
}