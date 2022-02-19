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


    }
}
