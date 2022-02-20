using AutoMapper;
using AutoMapper.QueryableExtensions;
using System.Diagnostics.CodeAnalysis;
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

        public async Task<Models.ProjectResponse> Create(Models.CreateProjectRequest projectRequest)
        {
            var createdProject = await _projectRepository.Create(
                _mapper.Map<Entities.Project>(projectRequest));
            return _mapper.Map<Models.ProjectResponse>(createdProject);
        }

        public async Task<Models.ProjectResponse?> Get(Guid projectId)
        {
            var foundProject = await _projectRepository.Get(projectId);
            if (foundProject == null)
                return null;
            return _mapper.Map<Models.ProjectResponse>(foundProject);
        }

        public async Task<Models.ProjectResponse?> Update(Guid projectId, Models.UpdateProjectRequest projectRequest)
        {
            var response =  await _projectRepository.Update(_mapper.Map<Entities.Project>(projectRequest));
            if (response == null)
                return null;
            return _mapper.Map<Models.ProjectResponse>(response);
        }

        public async Task<Models.ProjectResponse?> Delete(Guid projectId)
        {
            var foundProject = await _projectRepository.Get(projectId);
            if (foundProject == null)
                return null;
            var response = await _projectRepository.Delete(projectId);
            return _mapper.Map<Models.ProjectResponse>(response);
        }

        public async Task<IQueryable<Models.ProjectResponse>> GetAll()
        {
            var response = await _projectRepository.All();
            return response.ProjectTo<Models.ProjectResponse>(_mapper.ConfigurationProvider);
        }

        public async Task<int> SaveChanges()
        {
            return await _projectRepository.Save();
        }
    }
}
