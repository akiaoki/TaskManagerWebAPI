namespace TaskManagerWebAPI.Services
{
    public interface IProjectService
    {

        public Task<Models.ProjectResponse> CreateProject(Models.CreateProjectRequest projectRequest);

        // throws
        public Task<int> SaveProjectChanges();

    }
}
