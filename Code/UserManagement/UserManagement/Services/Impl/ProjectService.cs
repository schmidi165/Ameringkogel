using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using UserManagement.Data;
using UserManagement.Models.DTOs;

namespace UserManagement.Services.Impl
{
    public class ProjectService : IProjectService
    {
        private readonly MSDbContext _dbContext;
        private readonly Mapper _mapper;

        public ProjectService(MSDbContext dbContext, Mapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProjectDTO>> GetAllProjects()
        {
            return await _dbContext.Projects
                .ProjectTo<ProjectDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<ProjectDTO?> GetProjectById(int projectId)
        {
            return await _dbContext.Projects
                .Where(p => p.Id == projectId)
                .ProjectTo<ProjectDTO>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
        }
    }
}
