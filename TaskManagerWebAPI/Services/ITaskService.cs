namespace TaskManagerWebAPI.Services
{
    /// <summary>
    /// Service for managing Task responses
    /// </summary>
    public interface ITaskService
    {
        /// <inheritdoc cref="Repositories.Infrastructure.IGenericRepository{T}.Create(T)"/>
        /// <remarks>Might return null if such referenced projectId does not exist</remarks>
        public Task<Models.TaskResponse?> Create(Models.CreateTaskRequest taskRequest);

        /// <inheritdoc cref="Repositories.Infrastructure.IGenericRepository{T}.Get(Guid)"/>
        public Task<Models.TaskResponse?> Get(Guid taskId);

        /// <inheritdoc cref="Repositories.Infrastructure.IGenericRepository{T}.Update(T)"/>
        public Task<Models.TaskResponse?> Update(Guid taskId, Models.UpdateTaskRequest taskRequest);

        /// <inheritdoc cref="Repositories.Infrastructure.IGenericRepository{T}.Delete(Guid)"/>
        public Task<Models.TaskResponse?> Delete(Guid taskId);

        /// <inheritdoc cref="Repositories.Infrastructure.IGenericRepository{T}.All"/>
        public Task<IQueryable<Models.TaskResponse>> GetAll();

        /// <summary>
        /// Adds task to the specified project by ids
        /// </summary>
        /// <param name="taskId"><see cref="Guid"/> specifying the task to be updated.</param>
        /// <param name="projectId"><see cref="Guid"/> specifying the project to be referenced.</param>
        /// <returns>A <see cref="Tuple"/> of <see cref="Models.TaskResponse"/> and <see cref="Models.ProjectResponse"/>; both of them can be null if not found.</returns>
        public Task<Tuple<Models.TaskResponse?, Models.ProjectResponse?>> AddToProject(Guid taskId, Guid projectId);

        /// <summary>
        /// Removes task from current project
        /// </summary>
        /// <param name="taskId"><see cref="Guid"/> specifying the task to be updated.</param>
        /// <returns>Updated <see cref="Models.TaskResponse"/>.</returns>
        public Task<Models.TaskResponse?> RemoveFromProject(Guid taskId);

        /// <inheritdoc cref="Repositories.Infrastructure.IGenericRepository{T}.Save"/>
        public Task<int> SaveChanges();
    }
}
