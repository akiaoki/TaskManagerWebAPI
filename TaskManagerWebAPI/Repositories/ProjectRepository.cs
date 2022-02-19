using Microsoft.EntityFrameworkCore;
using TaskManagerWebAPI.Repositories.Infrastructure;

namespace TaskManagerWebAPI.Repositories
{
    public class ProjectRepository : GenericRepository<Entities.Project>, IProjectRepository
    {
        public ProjectRepository(ProjectDbContext dbContext) : base(dbContext)
        {

        }
    }
}
