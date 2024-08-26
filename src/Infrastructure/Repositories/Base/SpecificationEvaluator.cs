using Core.Specifications.Base;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Base
{
    public class SpecificationEvaluator : ISpecificationEvaluator
    {
        public static SpecificationEvaluator Default { get; } = new SpecificationEvaluator();

        public IQueryable<TResult> GetQuery<T, TResult>(IQueryable<T> query, ISpecification<T, TResult> specification) where T : class
        {
            query = GetQuery(query, (ISpecification<T>)specification);

            return specification.Selector is not null
                ? query.Select(specification.Selector)
                : query.SelectMany(specification.SelectorMany!);
        }

        public IQueryable<T> GetQuery<T>(IQueryable<T> query, ISpecification<T> specification) where T : class
        {
            if (specification.Criteria != null)
            {
                query = query.Where(specification.Criteria);
            }

            query = specification.Includes.Aggregate(query,
                (current, include) => current.Include(include));

            IOrderedQueryable<T>? orderedQuery = null;

            foreach (var orderBy in specification.OrderBy)
            {
                orderedQuery = orderedQuery == null
                    ? query.OrderBy(orderBy)
                    : orderedQuery.ThenBy(orderBy);
            }

            foreach (var orderByDescending in specification.OrderByDescending)
            {
                orderedQuery = orderedQuery == null
                    ? query.OrderByDescending(orderByDescending)
                    : orderedQuery.ThenByDescending(orderByDescending);
            }

            if (orderedQuery != null)
            {
                query = orderedQuery;
            }

            if (specification.AsNoTrackingWithIdentityResolution)
            {
                query = query.AsNoTrackingWithIdentityResolution();
            }
            else if (specification.AsNoTracking)
            {
                query = query.AsNoTracking();
            }

            if (specification.AsSplitQuery)
            {
                query = query.AsSplitQuery();
            }

            if (specification.Skip.HasValue && specification.Skip != 0)
            {
                query = query.Skip(specification.Skip.Value);
            }

            if (specification.Take.HasValue)
            {
                query = query.Take(specification.Take.Value);
            }

            return query;
        }
    }
}
