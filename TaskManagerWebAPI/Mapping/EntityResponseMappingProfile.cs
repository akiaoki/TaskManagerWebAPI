using AutoMapper;

namespace TaskManagerWebAPI.Mapping
{
    /// <summary>
    /// An AutoMapper profile for entities and models
    /// </summary>
    public class EntityResponseMappingProfile : Profile
    {
        /// <summary>
        /// Constructor
        /// </summary>
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
            CreateMap<Models.UpdateTaskRequest, Entities.Task>();
            CreateMap<Models.UpdateProjectRequest, Entities.Project>();
        }
    }
}
