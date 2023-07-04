using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using UserManagement.Data;
using UserManagement.Models.Conversions;
using UserManagement.Models.DTOs;

namespace UserManagement.Services.Impl
{
    public class WorkItemService : IWorkItemService
    {
        private readonly MSDbContext _dbContext;
        private readonly Mapper _mapper;

        public WorkItemService(MSDbContext dbContext, Mapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<WorkItemDTO> AddWorkItem(WorkItemDTO workItem)
        {
            var entity = workItem.ToModel();
            var result = _dbContext.WorkItems.Add(entity);

            await _dbContext.SaveChangesAsync();
            return result.Entity.ToDTO();
        }

        public async Task<WorkItemDTO> AddOrUpdateItem(WorkItemDTO workItem)
        {
            var entity = workItem.ToModel();
            var result = _dbContext.WorkItems.Attach(entity);

            await _dbContext.SaveChangesAsync();
            return result.Entity.ToDTO();
        }

        public async Task<IEnumerable<WorkItemDTO>> GetAllWorkItems()
        {
            return await _dbContext.WorkItems
                .ProjectTo<WorkItemDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<IEnumerable<WorkItemDTO>> GetAllWorkItemsLastChange(DateTime lastChanged)
        {
            return await _dbContext.WorkItems
                .Where(w => w.ModifiedAt > lastChanged || (w.User != null && w.User.ModifiedAt > lastChanged))
                .ProjectTo<WorkItemDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }
    }
}
