using Microsoft.EntityFrameworkCore;
using TaskManagerWebAPI.Repositories.Infrastructure;

namespace TaskManagerWebAPI.Repositories
{
    /// <summary>
    /// <inheritdoc cref="ITaskRepository"/>
    /// </summary>
    public class TaskRepository : GenericRepository<Entities.Task>, ITaskRepository
    {
        /// <summary/>
        public TaskRepository(TaskManagerDbContext dbContext) : base(dbContext)
        {

        }
    }
}
