namespace TaskManagerWebAPI.Services
{
    /// <summary>
    /// Service for managing Project responses
    /// </summary>
    public interface IProjectService
    {
        /// <inheritdoc cref="Repositories.Infrastructure.IGenericRepository{T}.Create(T)"/>
        public Task<Models.ProjectResponse> Create(Models.CreateProjectRequest projectRequest);

        /// <inheritdoc cref="Repositories.Infrastructure.IGenericRepository{T}.Get(Guid)"/>
        public Task<Models.ProjectResponse?> Get(Guid projectId);

        /// <inheritdoc cref="Repositories.Infrastructure.IGenericRepository{T}.Update(T)"/>
        public Task<Models.ProjectResponse?> Update(Guid projectId, Models.UpdateProjectRequest projectRequest);

        /// <inheritdoc cref="Repositories.Infrastructure.IGenericRepository{T}.Delete(Guid)"/>
        public Task<Models.ProjectResponse?> Delete(Guid projectId);

        /// <inheritdoc cref="Repositories.Infrastructure.IGenericRepository{T}.All"/>
        public Task<IQueryable<Models.ProjectResponse>> GetAll();

        /// <summary>
        /// Gets tasks for this project.
        /// </summary>
        /// <param name="projectId"><see cref="Guid"/> specifying the project to be examined</param>
        /// <returns><see cref="IQueryable"/> of <see cref="Models.TaskResponse"/> containing all referenced tasks</returns>
        public Task<IQueryable<Models.TaskResponse>?> GetProjectTasks(Guid projectId);

        /// <inheritdoc cref="Repositories.Infrastructure.IGenericRepository{T}.Save"/>
        public Task<int> SaveChanges();

    }
}
