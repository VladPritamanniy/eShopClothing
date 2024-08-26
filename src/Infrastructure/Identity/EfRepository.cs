using Core.Repositories.Base;
using Infrastructure.Repositories.Base;

namespace Infrastructure.Identity
{
    public class EfRepository<T> : Repository<T>, IReadRepository<T>, IRepository<T> where T : class
    {
        public EfRepository(AppIdentityDbContext dbContext) 
            : base(dbContext)
        {
        }
    }
}
