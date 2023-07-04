using Microsoft.AspNetCore.Mvc;
using UserManagement.Models.DTOs;
using UserManagement.Services;

namespace UserManagement.Controllers
{
    [ApiController]
    [Route("work-items")]
    public class WorkItemController: ControllerBase
    {
        private readonly IWorkItemService _workItemService;

        public WorkItemController(IWorkItemService workItemService)
        {
            _workItemService = workItemService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWorkItems()
        {
            var workItems = await _workItemService.GetAllWorkItems();
            return Ok(workItems);
        }

        [HttpPost]
        public async Task<IActionResult> AddWorkItem(WorkItemDTO workItem)
        {
            var result = await _workItemService.AddWorkItem(workItem);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> AddOrUpdateItem(WorkItemDTO workItem)
        {
            var result = await _workItemService.AddOrUpdateItem(workItem);
            return Ok(result);
        }
    }
}
