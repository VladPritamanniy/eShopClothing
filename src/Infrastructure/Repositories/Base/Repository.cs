using Core.Repositories.Base;
using Core.Specifications.Base;
using Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Base
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly AppIdentityDbContext _dbContext;
        private readonly ISpecificationEvaluator specificationEvaluator;

        public Repository(AppIdentityDbContext dbContext, ISpecificationEvaluator specificationEvaluator)
        {
            _dbContext = dbContext;
            this.specificationEvaluator = specificationEvaluator;
        }

        public async Task<IReadOnlyList<T>> GetAll()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task<IReadOnlyList<T>> Get(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }

        public async Task<TResult?> Get<TResult>(ISpecification<T, TResult> spec)
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }

        public async Task<T?> FirstOrDefaultAsync(ISpecification<T> specification)
        {
            return await ApplySpecification(specification).FirstOrDefaultAsync();
        }

        public async Task<TResult?> FirstOrDefaultAsync<TResult>(ISpecification<T, TResult> specification)
        {
            return await ApplySpecification(specification).FirstOrDefaultAsync();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public void Add(T entity)
        {
            _dbContext.Set<T>().Add(entity);
        }

        public async Task AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
        }

        public virtual async Task AddRangeAsync(IEnumerable<T> entities)
        {
            _dbContext.Set<T>().AddRange(entities);
            await SaveChangesAsync(); ;
        }

        public async Task Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            await SaveChangesAsync();
        }

        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return specificationEvaluator.GetQuery(_dbContext.Set<T>().AsQueryable(), spec);
        }

        private IQueryable<TResult> ApplySpecification<TResult>(ISpecification<T, TResult> spec)
        {
            return specificationEvaluator.GetQuery(_dbContext.Set<T>().AsQueryable(), spec);
        }
    }
}
