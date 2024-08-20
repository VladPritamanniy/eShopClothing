using System.Linq.Expressions;

namespace Core.Specifications.Base
{
    public class Specification<T, TResult> : Specification<T>, ISpecification<T, TResult>
    {
        public Expression<Func<T, TResult>>? Selector { get; set; }

        public Expression<Func<T, IEnumerable<TResult>>>? SelectorMany { get; set; }

        protected void ApplySelector(Expression<Func<T, TResult>> selectorExpression)
        {
            Selector = selectorExpression;
        }

        protected void ApplySelectorMany(Expression<Func<T, IEnumerable<TResult>>> selectorManyExpression)
        {
            SelectorMany = selectorManyExpression;
        }
    }

    public class Specification<T> : ISpecification<T>
    {
        public Expression<Func<T, bool>> Criteria { get; private set; }
        public List<Expression<Func<T, object>>> Includes { get; private set; } = new List<Expression<Func<T, object>>>();
        public List<Expression<Func<T, object>>> OrderBy { get; private set; } = new List<Expression<Func<T, object>>>();
        public List<Expression<Func<T, object>>> OrderByDescending { get; private set; } = new List<Expression<Func<T, object>>>();

        public bool AsNoTracking { get; private set; }
        public bool AsSplitQuery { get; private set; }
        public bool AsNoTrackingWithIdentityResolution { get; private set; }
        public int? Take { get; private set; }
        public int? Skip { get; private set; }

        protected void AddCriteria(Expression<Func<T, bool>> criteriaExpression)
        {
            Criteria = criteriaExpression;
        }

        protected void AddInclude(Expression<Func<T, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }

        protected void ApplyOrderBy(Expression<Func<T, object>> orderByExpression)
        {
            OrderBy.Add(orderByExpression);
        }

        protected void ApplyOrderByDescending(Expression<Func<T, object>> orderByDescendingExpression)
        {
            OrderByDescending.Add(orderByDescendingExpression);
        }

        protected void ApplyAsNoTracking(bool asNoTracking)
        {
            AsNoTracking = asNoTracking;
        }

        protected void ApplyAsSplitQuery(bool asSplitQuery)
        {
            AsSplitQuery = asSplitQuery;
        }

        protected void ApplyAsNoTrackingWithIdentityResolution(bool asNoTrackingWithIdentityResolution)
        {
            AsNoTrackingWithIdentityResolution = asNoTrackingWithIdentityResolution;
        }

        protected void AddTake(int take)
        {
            Take = take;
        }

        protected void AddSkip(int skip)
        {
            Skip = skip;
        }
    }
}
