namespace Core.Repositories.Base
{
    public interface IRepositoryBase<T> : IReadRepositoryBase<T> where T : class
    {
        void Add(T entity);
        Task AddAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entities);
        Task Delete(T entity);
        Task<int> SaveChangesAsync();
    }
}
