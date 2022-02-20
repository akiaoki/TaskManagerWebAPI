using Microsoft.AspNetCore.Mvc;
using TaskManagerWebAPI.Services;

namespace TaskManagerWebAPI.Controllers
{
    /// <summary>
    /// A controller for managing <see cref="IProjectService"/> models
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private IProjectService _projectService;

        /// <summary>
        /// Controller constructor
        /// </summary>
        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
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

        /// <summary>
        /// Gets a project by id.
        /// </summary>
        /// <param name="projectId"><see cref="Guid"/> specifying the project to be found.</param>
        /// <returns>
        /// If <see cref="StatusCodes.Status200OK"/>, returns a found <see cref="Models.ProjectResponse"/>.<para/>
        /// If <see cref="StatusCodes.Status404NotFound"/>, returns an error with projectId that was not found.
        /// </returns>
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

        /// <summary>
        /// Updates a project by id with the specified <see cref="Models.UpdateProjectRequest"/>
        /// </summary>
        /// <param name="projectId"><see cref="Guid"/> specifying the project to be updated.</param>
        /// <param name="updateProjectRequest"><see cref="Models.UpdateProjectRequest"/> with data that should be applied.</param>
        /// <remarks>Note that <see cref="Models.UpdateProjectRequest"/> should have the same projectId as stated in the request.</remarks>
        /// <returns>
        /// If <see cref="StatusCodes.Status200OK"/>, returns an updated <see cref="Models.ProjectResponse"/>.<para/>
        /// If <see cref="StatusCodes.Status400BadRequest"/>, returns an object specifying errors in the request.<para/>
        /// If <see cref="StatusCodes.Status404NotFound"/>, returns an error with projectId that was not found.
        /// </returns>
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

        /// <summary>
        /// Deletes a project by id.
        /// </summary>
        /// <param name="projectId"><see cref="Guid"/> specifying the project to be deleted.</param>
        /// <remarks>Note that the specified project should have no referenced tasks.</remarks>
        /// <returns>
        /// If <see cref="StatusCodes.Status200OK"/>, returns a deleted <see cref="Models.ProjectResponse"/>.<para/>
        /// If <see cref="StatusCodes.Status400BadRequest"/>, returns an object specifying errors in the request.<para/>
        /// If <see cref="StatusCodes.Status404NotFound"/>, returns an error with projectId that was not found.
        /// </returns>
        [HttpDelete("{projectId}")]
        [ProducesResponseType(typeof(Models.ProjectResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(Guid projectId)
        {
            var projectTasks = await _projectService.GetProjectTasks(projectId);
            if (projectTasks == null)
            {
                return ProjectNotFound(projectId);
            }
            if (projectTasks.Count() != 0)
            {
                return BadRequest($"The specified projectId ({projectId}) has referenced tasks to it. Remove them first and try again");
            }

            var response = await _projectService.Delete(projectId);
            await _projectService.SaveChanges();

            if (response == null)
            {
                return ProjectNotFound(projectId);
            }

            return Ok(response);
        }

        /// <summary>
        /// Gets all existing projects.
        /// </summary>
        /// <returns>
        /// If <see cref="StatusCodes.Status200OK"/>, returns an <see cref="IQueryable"/> of <see cref="Models.ProjectResponse"/> containing all existing projects.
        /// </returns>
        [HttpGet]
        [ProducesResponseType(typeof(IQueryable<Models.ProjectResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetAll()
        {
            var response = await _projectService.GetAll();
            return Ok(response);
        }

        /// <summary>
        /// Gets all tasks referenced to the specified project.
        /// </summary>
        /// <param name="projectId"><see cref="Guid"/> specifying the project to be examined</param>
        /// <returns>
        /// If <see cref="StatusCodes.Status200OK"/>, returns an <see cref="IQueryable"/> of <see cref="Models.ProjectResponse"/> containing all referenced tasks.
        /// If <see cref="StatusCodes.Status404NotFound"/>, returns an error with projectId that was not found.
        /// </returns>
        [HttpGet("{projectId}/Tasks")]
        [ProducesResponseType(typeof(IQueryable<Models.TaskResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetTasks(Guid projectId)
        {
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
