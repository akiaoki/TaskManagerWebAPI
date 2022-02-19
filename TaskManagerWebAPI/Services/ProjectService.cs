using AutoMapper;
using TaskManagerWebAPI.Repositories;

namespace TaskManagerWebAPI.Services
{
    public class ProjectService : IProjectService
    {

        private readonly IMapper _mapper;
        private readonly IProjectRepository _projectRepository;

        public ProjectService(IMapper mapper, IProjectRepository projectRepository)
        {
            _mapper = mapper;
            _projectRepository = projectRepository;
        }

        public async Task<Models.ProjectResponse> CreateProject(Models.CreateProjectRequest projectRequest)
        {
            var createdProject = await _projectRepository.Create(
                _mapper.Map<Entities.Project>(projectRequest));
            return _mapper.Map<Models.ProjectResponse>(createdProject);
        }

        public async Task<int> SaveProjectChanges()
        {
            return await _projectRepository.Save();
        }
    }
}
