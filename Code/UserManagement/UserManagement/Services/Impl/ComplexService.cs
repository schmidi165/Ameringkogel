using UserManagement.Data;
using UserManagement.Models.Conversions;
using UserManagement.Models.DTOs;

namespace UserManagement.Services.Impl
{
    public class ComplexService
    {
        private readonly MSDbContext _dbContext;
        private readonly IUserService _userService;
        private readonly IWorkItemService _workItemService;

        public ComplexService(MSDbContext dbContext, IUserService userService, IWorkItemService workItemService)
        {
            _dbContext = dbContext;
            _userService = userService;
            _workItemService = workItemService;
        }

        public async Task AssignWorkItemToLeastOccupiedWorker(WorkItemDTO workItem)
        {
            if (workItem.Worker != null)
                throw new InvalidOperationException("workItem has already a worker!");

            var workItems = await _workItemService.GetAllWorkItems();
            
            var workItemsPerUser = workItems
                .GroupBy(w => w.Worker.Id)
                .ToDictionary(g => g.Key, g => g.Count());

            int minimumItems = int.MaxValue;
            int? userId = null;
            foreach(var itemMap in workItemsPerUser)
            {
                if (itemMap.Value < minimumItems)
                {
                    userId = itemMap.Key;
                    minimumItems = itemMap.Value;
                }
            }

            if(userId != null)
            {
                var user = await _userService.GetUserById(userId.Value);
                workItem.Worker = user;

                _dbContext.Attach(workItem.ToModel());
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
