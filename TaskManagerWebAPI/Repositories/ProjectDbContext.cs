using Microsoft.EntityFrameworkCore;

namespace TaskManagerWebAPI.Repositories
{
    public class ProjectDbContext : DbContext
    {

        public DbSet<Entities.Project> Projects { get; set; }

        public ProjectDbContext(DbContextOptions<ProjectDbContext> options) : base(options)
        {

        }

    }
}
