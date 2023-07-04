using Microsoft.AspNetCore.Mvc;
using UserManagement.Services;

namespace UserManagement.Controllers
{
    [ApiController]
    [Route("user-groups")]
    public class UserGroupController : ControllerBase
    {
        private readonly IUserGroupService _userGroupService;

        public UserGroupController(IUserGroupService userGroupService)
        {
            _userGroupService = userGroupService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUserGroups()
        {
            var userGroups = await _userGroupService.GetAllUserGroups();
            return Ok(userGroups);
        }
    }
}
