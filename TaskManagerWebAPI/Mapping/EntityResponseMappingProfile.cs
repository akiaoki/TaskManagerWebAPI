using AutoMapper;

namespace TaskManagerWebAPI.Mapping
{
    public class EntityResponseMappingProfile : Profile
    {
        public EntityResponseMappingProfile()
        {
            // Entity -> Model
            CreateMap<Entities.Task, Models.CreateTaskRequest>();
            CreateMap<Entities.Task, Models.TaskResponse>();
            CreateMap<Entities.Project, Models.CreateProjectRequest>();
            CreateMap<Entities.Project, Models.ProjectResponse>();

            // Model -> Entity
            CreateMap<Models.CreateTaskRequest, Entities.Task>();
            CreateMap<Models.CreateProjectRequest, Entities.Project>();
        }
    }
}
