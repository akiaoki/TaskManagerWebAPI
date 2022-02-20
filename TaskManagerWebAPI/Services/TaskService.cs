using AutoMapper;
using AutoMapper.QueryableExtensions;
using TaskManagerWebAPI.Models;
using TaskManagerWebAPI.Repositories;

namespace TaskManagerWebAPI.Services
{
    public class TaskService : ITaskService
    {
        private readonly IMapper _mapper;
        private readonly ITaskRepository _taskRepository;
        private readonly IProjectRepository _projectRepository;

        public TaskService(IMapper mapper, ITaskRepository taskRepository, IProjectRepository projectRepository)
        {
            _mapper = mapper;
            _taskRepository = taskRepository;
            _projectRepository = projectRepository;
        }

        public async Task<Models.TaskResponse?> Create(Models.CreateTaskRequest taskRequest)
        {
            if (taskRequest.ProjectId != null)
            {
                if ((await _projectRepository.Get(taskRequest.ProjectId.Value)) == null) // Such projectId does not exist
                    return null;
            }

            var createdTask = await _taskRepository.Create(
                _mapper.Map<Entities.Task>(taskRequest));
            return _mapper.Map<Models.TaskResponse>(createdTask);
        }

        public async Task<Models.TaskResponse?> Get(Guid taskId)
        {
            var foundTask = await _taskRepository.Get(taskId);
            if (foundTask == null)
                return null;
            return _mapper.Map<Models.TaskResponse>(foundTask);
        }

        public async Task<Models.TaskResponse?> Update(Guid taskId, Models.UpdateTaskRequest taskRequest)
        {
            var response = await _taskRepository.Update(_mapper.Map<Entities.Task>(taskRequest));
            if (response == null)
                return null;
            return _mapper.Map<Models.TaskResponse>(response);
        }

        public async Task<Models.TaskResponse?> Delete(Guid taskId)
        {
            var foundTask = await _taskRepository.Get(taskId);
            if (foundTask == null)
                return null;
            var response = await _taskRepository.Delete(taskId);
            return _mapper.Map<Models.TaskResponse>(response);
        }

        public async Task<IQueryable<Models.TaskResponse>> GetAll()
        {
            var response = await _taskRepository.All();
            return response.ProjectTo<Models.TaskResponse>(_mapper.ConfigurationProvider);
        }

        public async Task<Tuple<Models.TaskResponse?, Models.ProjectResponse?>> AddToProject(Guid taskId, Guid projectId)
        {
            Models.TaskResponse? taskResponse = null;
            Models.ProjectResponse? projectResponse = null;

            var foundTask = await _taskRepository.Get(taskId);
            var foundProject = await _projectRepository.Get(projectId);
            
            if (foundTask == null || foundProject == null)
            {
                if (foundTask != null)
                    taskResponse = _mapper.Map<Models.TaskResponse>(foundTask);
                if (foundProject != null)
                    projectResponse = _mapper.Map<Models.ProjectResponse>(foundProject);
                return new Tuple<Models.TaskResponse?, Models.ProjectResponse?>(taskResponse, projectResponse);
            }

            foundTask.ProjectId = projectId;
            await _taskRepository.Save();

            return new Tuple<Models.TaskResponse?, Models.ProjectResponse?>(
                _mapper.Map<Models.TaskResponse>(foundTask),
                _mapper.Map<Models.ProjectResponse>(foundProject));
        }

        public async Task<Models.TaskResponse?> RemoveFromProject(Guid taskId)
        {
            var foundTask = await _taskRepository.Get(taskId);
            if (foundTask == null)
                return null;

            foundTask.ProjectId = null;
            await _taskRepository.Save();

            return _mapper.Map<Models.TaskResponse>(foundTask);
        }

        public async Task<int> SaveChanges()
        {
            return await _taskRepository.Save();
        }
    }
}
