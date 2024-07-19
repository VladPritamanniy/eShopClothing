using Core.Entities.Base;
using Core.Specification.Base;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Identity
{
    public class EfCoreQueryBuilder<TEntity> : IQueryBuilder<TEntity>
        where TEntity : BaseEntity
    {
        public IQueryable<TEntity> ApplyIncludes(IQueryable<TEntity> query, List<Expression<Func<TEntity, object>>> includes)
        {
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }
            return query;
        }

        public IQueryable<TEntity> ApplyNoTracking(IQueryable<TEntity> query)
        {
            return query.AsNoTracking();
        }
    }
}
