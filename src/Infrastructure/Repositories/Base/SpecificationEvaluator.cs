using Core.Specifications.Base;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Base
{
    public class SpecificationEvaluator : ISpecificationEvaluator
    {
        public IQueryable<TResult> GetQuery<T, TResult>(IQueryable<T> query, ISpecification<T, TResult> specification) where T : class
        {
            query = GetQuery(query, (ISpecification<T>)specification);

            return specification.Selector is not null
                ? query.Select(specification.Selector)
                : throw new NullReferenceException("Selector specification is null. You must choose selector.");
        }

        public IQueryable<T> GetQuery<T>(IQueryable<T> query, ISpecification<T> specification) where T : class
        {
            if (specification.Criteria != null)
            {
                query = query.Where(specification.Criteria);
            }

            query = specification.Includes.Aggregate(query,
                (current, include) => current.Include(include));


            if (specification.OrderBy != null)
            {
                query = query.OrderBy(specification.OrderBy);
            }
            else if (specification.OrderByDescending != null)
            {
                query = query.OrderByDescending(specification.OrderByDescending);
            }

            return query;
        }
    }
}
