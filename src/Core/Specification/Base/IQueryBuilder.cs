using Core.Entities.Base;
using System.Linq.Expressions;

namespace Core.Specification.Base
{
    public interface IQueryBuilder<TEntity>
        where TEntity : BaseEntity
    {
        IQueryable<TEntity> ApplyIncludes(IQueryable<TEntity> query, List<Expression<Func<TEntity, object>>> includes);
        IQueryable<TEntity> ApplyNoTracking(IQueryable<TEntity> query);
    }
}
