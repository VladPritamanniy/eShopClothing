using System.Linq.Expressions;

namespace Core.Specifications.Base
{
    public class Specification<T, TResult> : Specification<T>, ISpecification<T, TResult>
    {
        public Expression<Func<T, TResult>>? Selector { get; set; }

        protected virtual void ApplySelector(Expression<Func<T, TResult>> selectorExpression)
        {
            Selector = selectorExpression;
        }
    }

    public class Specification<T> : ISpecification<T>
    {
        public Expression<Func<T, bool>> Criteria { get; set; }
        public List<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();
        public Expression<Func<T, object>> OrderBy { get; set; }
        public Expression<Func<T, object>> OrderByDescending { get; set; }

        protected virtual void AddCriteria(Expression<Func<T, bool>> criteriaExpression)
        {
            Criteria = criteriaExpression;
        }

        protected virtual void AddInclude(Expression<Func<T, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }

        protected virtual void ApplyOrderBy(Expression<Func<T, object>> orderByExpression)
        {
            OrderBy = orderByExpression;
        }

        protected virtual void ApplyOrderByDescending(Expression<Func<T, object>> orderByDescendingExpression)
        {
            OrderByDescending = orderByDescendingExpression;
        }
    }
}
