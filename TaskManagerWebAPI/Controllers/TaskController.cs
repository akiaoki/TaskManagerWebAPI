using Microsoft.AspNetCore.Mvc;
using TaskManagerWebAPI.Services;

namespace TaskManagerWebAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(Models.TaskResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Create([FromBody] Models.CreateTaskRequest createTaskRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var response = await _taskService.Create(createTaskRequest);
            await _taskService.SaveChanges();
            return Created("api/v1/Task/" + response.Id, response);
        }

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

        [HttpGet]
        [ProducesResponseType(typeof(IQueryable<Models.TaskResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetAll()
        {
            var response = await _taskService.GetAll();
            return Ok(response);
        }

        private NotFoundObjectResult TaskNotFound(Guid taskId) => NotFound($"Task with the given taskId ({taskId}) was not found");
        private NotFoundObjectResult ProjectNotFound(Guid projectId) => NotFound($"Project with the given projectId ({projectId}) was not found");
    }
}
