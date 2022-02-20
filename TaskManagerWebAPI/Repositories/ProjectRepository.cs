using Microsoft.EntityFrameworkCore;
using TaskManagerWebAPI.Repositories.Infrastructure;

namespace TaskManagerWebAPI.Repositories
{
    /// <summary>
    /// <inheritdoc cref="IProjectRepository"/>
    /// </summary>
    public class ProjectRepository : GenericRepository<Entities.Project>, IProjectRepository
    {
        /// <summary/>
        public ProjectRepository(TaskManagerDbContext dbContext) : base(dbContext)
        {

        }
    }
}
