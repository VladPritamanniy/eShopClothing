using Core.Entities.Base;
using Core.Specification.Base;

namespace Core.Repositories.Base
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        Task<List<TEntity>> GetById(string id, Specification<TEntity> specification);
    }
}
