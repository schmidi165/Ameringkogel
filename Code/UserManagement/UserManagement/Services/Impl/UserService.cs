using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using UserManagement.Data;
using UserManagement.Models.Conversions;
using UserManagement.Models.DTOs;

namespace UserManagement.Services.Impl
{
    public class UserService : IUserService
    {
        private readonly MSDbContext _dbContext;
        private readonly Mapper _mapper;

        public UserService(MSDbContext dbContext, Mapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserDTO>> GetAllUsers()
        {
            return await _dbContext.Users
                .ProjectTo<UserDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<IEnumerable<UserDTO>> GetAllUsersWithinWorkGroup(int groupId)
        {
            return await _dbContext.Users
                .Where(u => u.UserGroupId == groupId)
                .ProjectTo<UserDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<UserDTO> GetUserById(int userId)
        {
            return await _dbContext.Users
                .Where(u => u.Id == userId)
                .ProjectTo<UserDTO>(_mapper.ConfigurationProvider)
                .SingleAsync();
        }

        public async Task<UserDTO> UpdateUser(UserDTO dto)
        {
            var model = dto.ToModel();

            var entity = _dbContext.Attach(model);
            await _dbContext.SaveChangesAsync();

            return entity.Entity.ToDTO();
        }
    }
}
