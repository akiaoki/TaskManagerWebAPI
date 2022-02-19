using AutoMapper;
using System.Diagnostics.CodeAnalysis;
using TaskManagerWebAPI.Models;
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

        public async Task<Models.IndexedProjectResponse> Create(Models.CreateProjectRequest projectRequest)
        {
            var createdProject = await _projectRepository.Create(
                _mapper.Map<Entities.Project>(projectRequest));
            return _mapper.Map<Models.IndexedProjectResponse>(createdProject);
        }

        public async Task<ProjectResponse?> Get(Guid projectId)
        {
            var foundProject = await _projectRepository.Get(projectId);
            if (foundProject == null)
                return null;
            return _mapper.Map<Models.ProjectResponse>(foundProject);
        }

        public async Task<ProjectResponse?> Update(Guid projectId, UpdateProjectRequest projectRequest)
        {
            var foundProject = await _projectRepository.Get(projectId);
            if (foundProject == null)
                return null;
            var response =  await _projectRepository.Update(_mapper.Map<Entities.Project>(projectRequest));
            return _mapper.Map<Models.ProjectResponse>(response);
        }

        public async Task<ProjectResponse?> Delete(Guid projectId)
        {
            var foundProject = await _projectRepository.Get(projectId);
            if (foundProject == null)
                return null;
            var response = await _projectRepository.Delete(projectId);
            return _mapper.Map<Models.ProjectResponse>(response);
        }

        public async Task<int> SaveChanges()
        {
            return await _projectRepository.Save();
        }

        
    }
}
