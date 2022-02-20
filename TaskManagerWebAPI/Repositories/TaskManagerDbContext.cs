using Microsoft.EntityFrameworkCore;

namespace TaskManagerWebAPI.Repositories
{
    public class TaskManagerDbContext : DbContext
    {

        public DbSet<Entities.Task> Tasks { get; set; }
        public DbSet<Entities.Project> Projects { get; set; }

        public TaskManagerDbContext(DbContextOptions<TaskManagerDbContext> options) : base(options)
        {

        }


    }
}
