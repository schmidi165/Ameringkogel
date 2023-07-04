using Microsoft.AspNetCore.Mvc;
using UserManagement.Data;
using UserManagement.Services;

namespace UserManagement.Controllers
{
    [ApiController]
    [Route("projects")]
    public class ProjectsController: ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectsController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProjects()
        {
            var projects = await _projectService.GetAllProjects();
            return Ok(projects);
        }
    }
}
