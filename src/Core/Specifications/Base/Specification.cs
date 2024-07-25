using System.Linq.Expressions;
using Core.Entities.Base;

namespace Core.Specifications.Base
{
    public abstract class Specification<TEntity>
        where TEntity : BaseEntity
    {
        public Specification()
        {
        }

        public Specification(Expression<Func<TEntity, bool>> criteria)
        {
            Criteria = criteria;
        }

        public Expression<Func<TEntity, bool>>? Criteria { get; }

        public List<Expression<Func<TEntity, object>>>? Includes { get; } =
            new List<Expression<Func<TEntity, object>>>();

        public Expression<Func<TEntity, object>>? OrderBy { get; private set; }

        public Expression<Func<TEntity, object>>? Take { get; private set; }

        public Expression<Func<TEntity, object>>? Skip { get; private set; }

        public bool IsNoTracking { get; private set; }

        protected void AddInclude(Expression<Func<TEntity, object>> includeExpression)
        {
            Includes?.Add(includeExpression);
        }

        protected void AddOrderBy(Expression<Func<TEntity, object>> orderByExpression)
        {
            OrderBy = orderByExpression;
        }

        protected void ApplyPaging(Expression<Func<TEntity, object>> skip, Expression<Func<TEntity, object>> take)
        {
            Skip = skip;
            Take = take;
        }

        protected void ApplyNoTracking()
        {
            IsNoTracking = true;
        }
    }
}
