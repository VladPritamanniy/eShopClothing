using System.Linq.Expressions;

namespace Core.Specifications.Base
{
    public interface ISpecification<T, TResult> : ISpecification<T>
    {
        Expression<Func<T, TResult>>? Selector { get; }
        Expression<Func<T, IEnumerable<TResult>>>? SelectorMany { get; }
    }

    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> Criteria { get; }
        List<Expression<Func<T, object>>> Includes { get; }
        List<Expression<Func<T, object>>> OrderBy { get; }
        List<Expression<Func<T, object>>> OrderByDescending { get; }
        bool AsNoTracking { get; }
        bool AsSplitQuery { get; }
        bool AsNoTrackingWithIdentityResolution { get; }
        int? Take { get; }
        int? Skip { get; }
    }
}
