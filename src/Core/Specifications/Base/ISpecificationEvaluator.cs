namespace Core.Specifications.Base
{
    public interface ISpecificationEvaluator
    {
        IQueryable<TResult> GetQuery<T, TResult>(IQueryable<T> query, ISpecification<T, TResult> specification) where T : class;
        IQueryable<T> GetQuery<T>(IQueryable<T> query, ISpecification<T> specification) where T : class;
    }
}
