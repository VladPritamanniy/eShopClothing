using Core.Entities.Base;
using Core.Specifications.Base;

namespace Core.Repositories.Base
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        Task<IReadOnlyList<TEntity>> GetAll();
        Task<IReadOnlyList<TEntity>> GetAll(Specification<TEntity> specification);
        Task Add(TEntity entity);
    }
}
