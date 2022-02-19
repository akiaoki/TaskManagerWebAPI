using Microsoft.AspNetCore.Mvc;
using TaskManagerWebAPI.Services;

namespace TaskManagerWebAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private IProjectService _projectService;

        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(Models.ProjectResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Create([FromBody] Models.CreateProjectRequest createProjectRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var response = await _projectService.Create(createProjectRequest);
            await _projectService.SaveChanges();
            return Created("api/v1/Projects/" + response.Id, response);
        }

        [HttpGet("{projectId}")]
        [ProducesResponseType(typeof(Models.ProjectResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Get(Guid projectId)
        {
            var response = await _projectService.Get(projectId);
            if (response == null)
            {
                return NotFound($"Project with id={projectId} was not found");
            }
            return Ok(response);
        }

        [HttpPut("{projectId}")]
        [ProducesResponseType(typeof(Models.ProjectResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Update(Guid projectId, [FromBody] Models.CreateProjectRequest updateProjectRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

        }
    }
}
