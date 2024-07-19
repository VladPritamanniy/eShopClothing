using Core.Entities;
using Core.Repositories;
using Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Core.Specification.Base;

namespace Infrastructure.Repositories
{
    public class ClothingRepository : IClothingRepository
    {
        private AppIdentityDbContext _context;
        private readonly IQueryBuilder<Clothing> _queryBuilder;

        public ClothingRepository(AppIdentityDbContext context, IQueryBuilder<Clothing> queryBuilder)
        {
            _context = context;
            _queryBuilder = queryBuilder;
        }

        public async Task<List<Clothing>> GetById(string id, Specification<Clothing> specification)
        {
            return await SpecificationQueryBuilder.GetQuery(_context.Clothing, specification, _queryBuilder).ToListAsync();
        }
    }
}
