namespace TaskManagerWebAPI.Services
{
    public interface IProjectService
    {

        public Task<Models.ProjectResponse> Create(Models.CreateProjectRequest projectRequest);

        public Task<Models.ProjectResponse?> Get(Guid projectId);

        public Task<Models.ProjectResponse?> Update(Guid projectId, Models.CreateProjectRequest projectRequest);

        public Task<int> SaveChanges();

    }
}
