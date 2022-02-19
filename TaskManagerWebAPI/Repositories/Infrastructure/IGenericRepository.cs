namespace TaskManagerWebAPI.Repositories.Infrastructure
{
    public interface IGenericRepository<T> where T : Entities.EntityBase
    {

        public Task<T> Create(T entity);

        public Task<T?> Get(Guid id);

        public Task<T?> Update(T entity);

        public Task<T?> Delete(Guid id);

        public Task<int> Save();

        public Task<IQueryable<T>> All();

    }
}
