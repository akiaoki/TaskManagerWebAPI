using TaskManagerWebAPI.Repositories.Infrastructure;

namespace TaskManagerWebAPI.Repositories
{
    /// <summary>
    /// Repository containing Task entities
    /// </summary>
    public interface ITaskRepository : IGenericRepository<Entities.Task>
    {



    }
}
