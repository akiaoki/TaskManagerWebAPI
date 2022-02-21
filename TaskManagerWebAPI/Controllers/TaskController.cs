using Microsoft.AspNetCore.Mvc;
using TaskManagerWebAPI.Services;

namespace TaskManagerWebAPI.Controllers
{
    /// <summary>
    /// A controller for managing <see cref="ITaskService"/> models
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private ITaskService _taskService;

        /// <summary>
        /// Controller constructor
        /// </summary>
        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        /// <summary>
        /// Creates a new task.
        /// </summary>
        /// <param name="createTaskRequest"><see cref="Models.CreateTaskRequest"/> specifying the task to be created.</param>
        /// <returns>
        /// If <see cref="StatusCodes.Status201Created"/>, returns a newly created <see cref="Models.TaskResponse"/>.<para/>
        /// If <see cref="StatusCodes.Status400BadRequest"/>, returns an object specifying errors in the request.
        /// </returns>
        [HttpPost]
        [ProducesResponseType(typeof(Models.TaskResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Create([FromBody] Models.CreateTaskRequest createTaskRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var response = await _taskService.Create(createTaskRequest);
            await _taskService.SaveChanges();
            if (response == null)
            {
                return ProjectNotFound(createTaskRequest.ProjectId.Value);
            }
            return Created("api/v1/Task/" + response.Id, response);
        }

        /// <summary>
        /// Gets a task by id.
        /// </summary>
        /// <param name="taskId"><see cref="Guid"/> specifying the task to be found.</param>
        /// <returns>
        /// If <see cref="StatusCodes.Status200OK"/>, returns a found <see cref="Models.TaskResponse"/>.<para/>
        /// If <see cref="StatusCodes.Status404NotFound"/>, returns an error with taskId that was not found.
        /// </returns>
        [HttpGet("{taskId}")]
        [ProducesResponseType(typeof(Models.TaskResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Get(Guid taskId)
        {
            var response = await _taskService.Get(taskId);
            if (response == null)
            {
                return TaskNotFound(taskId);
            }
            return Ok(response);
        }

        /// <summary>
        /// Updates a task by id with the specified <see cref="Models.UpdateTaskRequest"/>
        /// </summary>
        /// <param name="taskId"><see cref="Guid"/> specifying the task to be updated.</param>
        /// <param name="updateTaskRequest"><see cref="Models.UpdateTaskRequest"/> with data that should be applied.</param>
        /// <remarks>Note that <see cref="Models.UpdateTaskRequest"/> should have the same taskId as stated in the request.</remarks>
        /// <returns>
        /// If <see cref="StatusCodes.Status200OK"/>, returns an updated <see cref="Models.TaskResponse"/>.<para/>
        /// If <see cref="StatusCodes.Status400BadRequest"/>, returns an object specifying errors in the request.<para/>
        /// If <see cref="StatusCodes.Status404NotFound"/>, returns an error with taskId that was not found.
        /// </returns>
        [HttpPut("{taskId}")]
        [ProducesResponseType(typeof(Models.TaskResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Update(Guid taskId, [FromBody] Models.UpdateTaskRequest updateTaskRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (taskId != updateTaskRequest.Id)
            {
                return BadRequest($"Route projectId was different ({taskId}) from the stated one in the request body ({updateTaskRequest.Id})");
            }

            var response = await _taskService.Update(taskId, updateTaskRequest);
            await _taskService.SaveChanges();

            if (response == null)
            {
                return TaskNotFound(taskId);
            }

            return Ok(response);
        }

        /// <summary>
        /// Deletes a task by id.
        /// </summary>
        /// <param name="taskId"><see cref="Guid"/> specifying the task to be deleted.</param>
        /// <returns>
        /// If <see cref="StatusCodes.Status200OK"/>, returns a deleted <see cref="Models.TaskResponse"/>.<para/>
        /// If <see cref="StatusCodes.Status400BadRequest"/>, returns an object specifying errors in the request.<para/>
        /// If <see cref="StatusCodes.Status404NotFound"/>, returns an error with taskId that was not found.
        /// </returns>
        [HttpDelete("{taskId}")]
        [ProducesResponseType(typeof(Models.TaskResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(Guid taskId)
        {
            var response = await _taskService.Delete(taskId);
            await _taskService.SaveChanges();

            if (response == null)
            {
                return TaskNotFound(taskId);
            }

            return Ok(response);
        }

        /// <summary>
        /// Gets all existing tasks.
        /// </summary>
        /// <returns>
        /// If <see cref="StatusCodes.Status200OK"/>, returns an <see cref="IQueryable"/> of <see cref="Models.TaskResponse"/> containing all existing tasks.
        /// </returns>
        [HttpGet]
        [ProducesResponseType(typeof(IQueryable<Models.TaskResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetAll()
        {
            var response = await _taskService.GetAll();
            return Ok(response);
        }

        /// <summary>
        /// Adds task to the specified project.
        /// </summary>
        /// <param name="taskId"><see cref="Guid"/> of the task to be updated</param>
        /// <param name="projectId"><see cref="Guid"/> of the project to be referenced to</param>
        /// <returns>
        /// If <see cref="StatusCodes.Status200OK"/>, returns an updated <see cref="Models.TaskResponse"/>.<para/>
        /// If <see cref="StatusCodes.Status404NotFound"/>, returns an error with taskId or projectId that was not found.
        /// </returns>
        /// <remarks>Note that in case of both taskId and projectId not be found, firstly taskId will be returned and only projectId later.</remarks>
        [HttpPut("{taskId}/AddToProject/{projectId}")]
        [ProducesResponseType(typeof(Models.TaskResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> AddToProject(Guid taskId, Guid projectId)
        {
            var response = await _taskService.AddToProject(taskId, projectId);

            if (response.Item1 == null)
            {
                return TaskNotFound(taskId);
            }
            if (response.Item2 == null)
            {
                return ProjectNotFound(projectId);
            }

            return Ok(response.Item2);
        }

        /// <summary>
        /// Removes project reference from the task.
        /// </summary>
        /// <param name="taskId"><see cref="Guid"/> of the task to be updated</param>
        /// <returns>
        /// If <see cref="StatusCodes.Status200OK"/>, returns an updated <see cref="Models.TaskResponse"/>.<para/>
        /// If <see cref="StatusCodes.Status404NotFound"/>, returns an error with taskId that was not found.
        /// </returns>
        [HttpPost("{taskId}/RemoveFromProject")]
        [ProducesResponseType(typeof(Models.TaskResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> RemoveFromProject(Guid taskId)
        {
            var response = await _taskService.RemoveFromProject(taskId);

            if (response == null)
            {
                return TaskNotFound(taskId);
            }

            return Ok(response);
        }

        private NotFoundObjectResult TaskNotFound(Guid taskId) => NotFound($"Task with the given taskId ({taskId}) was not found");
        private NotFoundObjectResult ProjectNotFound(Guid projectId) => NotFound($"Project with the given projectId ({projectId}) was not found");
    }
}
