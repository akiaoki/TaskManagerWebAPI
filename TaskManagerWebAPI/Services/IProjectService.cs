namespace TaskManagerWebAPI.Services
{
    public interface IProjectService
    {

        public Task<Models.IndexedProjectResponse> Create(Models.CreateProjectRequest projectRequest);

        public Task<Models.ProjectResponse?> Get(Guid projectId);

        public Task<Models.ProjectResponse?> Update(Guid projectId, Models.UpdateProjectRequest projectRequest);

        public Task<Models.ProjectResponse?> Delete(Guid projectId);

        public Task<int> SaveChanges();

    }
}
