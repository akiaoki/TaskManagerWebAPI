using Microsoft.AspNetCore.Mvc;
using TaskManagerWebAPI.Services;

namespace TaskManagerWebAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private IProjectService _projectService;
        private ITaskService _taskService;

        /// <summary>
        /// Controller constructor
        /// </summary>
        public ProjectController(IProjectService projectService, ITaskService taskService)
        {
            _projectService = projectService;
            _taskService = taskService;
        }

        /// <summary>
        /// Creates a new project.
        /// </summary>
        /// <param name="createProjectRequest"><see cref="Models.CreateProjectRequest"/> specifying the project to be created.</param>
        /// <returns>
        /// If <see cref="StatusCodes.Status201Created"/>, returns a newly created <see cref="Models.ProjectResponse"/>.<para/>
        /// If <see cref="StatusCodes.Status400BadRequest"/>, returns an object specifying errors in the request.
        /// </returns>
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
            return Created("api/v1/Project/" + response.Id, response);
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
            await _projectService.SaveChanges();

            if (response == null)
            {
                return ProjectNotFound(projectId);
            }

            return Ok(response);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IQueryable<Models.ProjectResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetAll()
        {
            var response = await _projectService.GetAll();
            return Ok(response);
        }

        [HttpGet("{projectId}/Tasks")]
        [ProducesResponseType(typeof(IQueryable<Models.TaskResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetTasks(Guid projectId)
        {
            //var response = await _taskService.GetProjectTasks(projectId);
            var response = await _projectService.GetProjectTasks(projectId);
            if (response == null)
            {
                return ProjectNotFound(projectId);
            }
            return Ok(response);
        }

        private NotFoundObjectResult TaskNotFound(Guid taskId) => NotFound($"Task with the given taskId ({taskId}) was not found");
        private NotFoundObjectResult ProjectNotFound(Guid projectId) => NotFound($"Project with the given projectId ({projectId}) was not found");
    }
}
