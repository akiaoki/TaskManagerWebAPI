namespace TaskManagerWebAPI.Services
{
    public interface ITaskService
    {
        public Task<Models.TaskResponse> Create(Models.CreateTaskRequest taskRequest);

        public Task<Models.TaskResponse?> Get(Guid taskId);

        public Task<Models.TaskResponse?> Update(Guid taskId, Models.UpdateTaskRequest taskRequest);

        public Task<Models.TaskResponse?> Delete(Guid taskId);

        public Task<IQueryable<Models.TaskResponse>> GetAll();

        public Task<int> SaveChanges();
    }
}
