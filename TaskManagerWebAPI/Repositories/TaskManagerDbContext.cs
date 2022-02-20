using Microsoft.EntityFrameworkCore;

namespace TaskManagerWebAPI.Repositories
{
    /// <summary>
    /// A common <see cref="DbContext"/> for managing Tasks and Projects
    /// </summary>
    public class TaskManagerDbContext : DbContext
    {
        /// <summary/>
        public DbSet<Entities.Task> Tasks { get; set; }
        /// <summary/>
        public DbSet<Entities.Project> Projects { get; set; }

        /// <summary/>
        public TaskManagerDbContext(DbContextOptions<TaskManagerDbContext> options) : base(options)
        {

        }


    }
}
