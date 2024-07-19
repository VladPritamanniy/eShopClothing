using Core.Entities.Base;

namespace Core.Specification.Base
{
    public static class SpecificationQueryBuilder
    {
        public static IQueryable<TEntity> GetQuery<TEntity>(
            IQueryable<TEntity> inputQuery,
            Specification<TEntity> specification,
            IQueryBuilder<TEntity> queryBuilder)
            where TEntity : BaseEntity
        {
            var query = inputQuery;

            if (specification.Criteria != null)
            {
                query = query.Where(specification.Criteria);
            }

            if (specification.Includes != null)
            {
                query = queryBuilder.ApplyIncludes(query, specification.Includes);
            }

            if (specification.OrderBy != null)
            {
                query = query.OrderBy(specification.OrderBy);
            }

            if (specification.IsNoTracking)
            {
                query = queryBuilder.ApplyNoTracking(query);
            }

            return query;
        }
    }
}
