using Core.Repositories;
using Core.Specifications.Base;
using Infrastructure.Identity;
using Infrastructure.Repositories.Base;

namespace Infrastructure.Repositories
{
    public class TypeRepository : Repository<Core.Entities.Type>, ITypeRepository
    {
        public TypeRepository(AppIdentityDbContext dbContext, IQueryBuilder<Core.Entities.Type> queryBuilder) : base(dbContext, queryBuilder)
        {
        }
    }
}
