using Core.Entities;
using Core.Repositories;
using Infrastructure.Identity;
using Core.Specifications.Base;
using Infrastructure.Repositories.Base;
using Core.Specifications;

namespace Infrastructure.Repositories
{
    public class ClothingRepository : Repository<Clothing>, IClothingRepository
    {

        public ClothingRepository(AppIdentityDbContext dbContext, IQueryBuilder<Clothing> queryBuilder) : base(dbContext, queryBuilder)
        {
        }

        public async Task<IEnumerable<Clothing>> GetAllUserProductByUserId(string id)
        {
            var spec = new GetClothingListByUserIdSpecification(id);
            var ty = await GetAll(spec);
            return ty;
        }
    }
}
