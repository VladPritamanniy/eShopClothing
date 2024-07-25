using Core.Entities.Base;
using Core.Repositories.Base;
using Core.Specifications.Base;
using Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Base
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly AppIdentityDbContext _dbContext;
        private readonly IQueryBuilder<TEntity> _queryBuilder;
        public Repository(AppIdentityDbContext dbContext, IQueryBuilder<TEntity> queryBuilder)
        {
            _dbContext = dbContext;
            _queryBuilder = queryBuilder;
        }

        public async Task<IReadOnlyList<TEntity>> GetAll()
        {
            return await _dbContext.Set<TEntity>().ToListAsync();
        }

        public async Task<IReadOnlyList<TEntity>> GetAll(Specification<TEntity> specification)
        {
            return await ApplySpecification(specification).ToListAsync();
        }

        private IQueryable<TEntity> ApplySpecification(Specification<TEntity> specification)
        {
            return SpecificationQueryBuilder.GetQuery(_dbContext.Set<TEntity>(), specification, _queryBuilder);
        }
    }
}
