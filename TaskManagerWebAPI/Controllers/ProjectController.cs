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
        [ProducesResponseType(typeof(Models.IndexedProjectResponse), StatusCodes.Status201Created)]
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
                return ProjectNotFound(projectId);
            }
            return Ok(response);
        }

        [HttpPut("{projectId}")]
        [ProducesResponseType(typeof(Models.ProjectResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Update(Guid projectId, [FromBody] Models.UpdateProjectRequest updateProjectRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (projectId != updateProjectRequest.Id)
            {
                return BadRequest($"Route projectId was different ({projectId}) from the stated one in the request body ({updateProjectRequest.Id})");
            }

            var response = await _projectService.Update(projectId, updateProjectRequest);
            await _projectService.SaveChanges();

            if (response == null)
            {
                return ProjectNotFound(projectId);
            }

            return Ok(response);
        }

        [HttpDelete("{projectId}")]
        [ProducesResponseType(typeof(Models.ProjectResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(Guid projectId)
        {
            var response = await _projectService.Delete(projectId);

            if (response == null)
            {
                return ProjectNotFound(projectId);
            }

            return Ok(response);
        }

        private NotFoundObjectResult ProjectNotFound(Guid projectId) => NotFound($"Project with id={projectId} was not found");
    }
}
