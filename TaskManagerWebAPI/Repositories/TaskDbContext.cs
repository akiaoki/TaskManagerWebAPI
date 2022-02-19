using Microsoft.EntityFrameworkCore;

namespace TaskManagerWebAPI.Repositories
{
    public class TaskDbContext : DbContext
    {
        public DbSet<Entities.Task> Tasks { get; set; }

        public TaskDbContext(DbContextOptions<TaskDbContext> options) : base(options)
        {

        }
    }
}
