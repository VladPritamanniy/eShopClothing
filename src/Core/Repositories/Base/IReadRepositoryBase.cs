using Core.Specifications.Base;

namespace Core.Repositories.Base
{
    public interface IReadRepositoryBase<T> where T : class
    {
        Task<IReadOnlyList<T>> GetAll();
        Task<IReadOnlyList<T>> Get(ISpecification<T> spec);
        Task<T?> FirstOrDefaultAsync(ISpecification<T> specification);
        Task<TResult?> FirstOrDefaultAsync<TResult>(ISpecification<T, TResult> specification);
        Task<TResult?> Get<TResult>(ISpecification<T, TResult> specification);
    }
}
