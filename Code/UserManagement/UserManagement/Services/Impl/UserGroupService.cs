using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using UserManagement.Data;
using UserManagement.Models.DTOs;

namespace UserManagement.Services.Impl
{
    public class UserGroupService : IUserGroupService
    {
        private readonly MSDbContext _dbContext;
        private readonly Mapper _mapper;

        public UserGroupService(MSDbContext dbContext, Mapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserGroupDTO>> GetAllUserGroups()
        {
            return await _dbContext.UserGroups
                .ProjectTo<UserGroupDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }
    }
}
