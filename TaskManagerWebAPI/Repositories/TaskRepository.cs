using Microsoft.EntityFrameworkCore;
using TaskManagerWebAPI.Repositories.Infrastructure;

namespace TaskManagerWebAPI.Repositories
{
    public class TaskRepository : GenericRepository<Entities.Task>, ITaskRepository
    {
        public TaskRepository(TaskManagerDbContext dbContext) : base(dbContext)
        {

        }
    }
}
