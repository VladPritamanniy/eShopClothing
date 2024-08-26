using Core.Repositories.Base;
using Core.Specifications.Base;
using Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Base
{
    public class Repository<T> : IRepositoryBase<T> where T : class
    {
        private readonly AppIdentityDbContext _dbContext;
        private readonly ISpecificationEvaluator _specificationEvaluator;

        public Repository(AppIdentityDbContext dbContext)
            : this(dbContext, SpecificationEvaluator.Default)
        {
            _dbContext = dbContext;
        }

        public Repository(AppIdentityDbContext dbContext, ISpecificationEvaluator specificationEvaluator)
        {
            _dbContext = dbContext;
            this._specificationEvaluator = specificationEvaluator;
        }

        public async Task<IReadOnlyList<T>> GetAll()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task<IReadOnlyList<T>> ToListAsync(ISpecification<T> specification)
        {
            return await ApplySpecification(specification).ToListAsync();
        }

        public async Task<IReadOnlyList<TResult>> ToListAsync<TResult>(ISpecification<T, TResult> specification)
        {
            return await ApplySpecification(specification).ToListAsync();
        }

        public async Task<T?> FirstOrDefaultAsync(ISpecification<T> specification)
        {
            return await ApplySpecification(specification).FirstOrDefaultAsync();
        }

        public async Task<TResult?> FirstOrDefaultAsync<TResult>(ISpecification<T, TResult> specification)
        {
            return await ApplySpecification(specification).FirstOrDefaultAsync();
        }

        public async Task<T?> SingleOrDefaultAsync(ISpecification<T> specification)
        {
            return await ApplySpecification(specification).SingleOrDefaultAsync();
        }

        public async Task<TResult?> SingleOrDefaultAsync<TResult>(ISpecification<T, TResult> specification)
        {
            return await ApplySpecification(specification).SingleOrDefaultAsync();
        }

        public async Task<int> CountAsync(ISpecification<T> specification)
        {
            return await ApplySpecification(specification).CountAsync();
        }

        public async Task<int> CountAsync()
        {
            return await _dbContext.Set<T>().CountAsync();
        }

        public async Task<TResult[]?> ToArrayAsync<TResult>(ISpecification<T, TResult> specification)
        {
            return await ApplySpecification(specification).ToArrayAsync();
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
            await SaveChangesAsync();
        }

        public virtual async Task AddRangeAsync(IEnumerable<T> entities)
        {
            _dbContext.Set<T>().AddRange(entities);
            await SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _dbContext.Set<T>().Update(entity);

            await SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            await SaveChangesAsync();
        }

        private IQueryable<T> ApplySpecification(ISpecification<T> specification)
        {
            return _specificationEvaluator.GetQuery(_dbContext.Set<T>().AsQueryable(), specification);
        }

        private IQueryable<TResult> ApplySpecification<TResult>(ISpecification<T, TResult> specification)
        {
            return _specificationEvaluator.GetQuery(_dbContext.Set<T>().AsQueryable(), specification);
        }
    }
}
