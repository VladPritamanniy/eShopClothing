using Core.Entities;
using Core.Repositories;
using Core.Specifications.Base;
using Infrastructure.Identity;
using Infrastructure.Repositories.Base;

namespace Infrastructure.Repositories
{
    public class SizeRepository : Repository<Size>, ISizeRepository
    {
        public SizeRepository(AppIdentityDbContext dbContext, IQueryBuilder<Size> queryBuilder) : base(dbContext, queryBuilder)
        {
        }
    }
}
