using Application.Interfaces;
using Core.Entities;
using Core.Exceptions;
using Core.Repositories.Base;
using Core.Specifications;

namespace Application.Services
{
    public class PermissionService : IPermissionService
    {
        private readonly IRepository<Clothing> _clothingRepository;

        public PermissionService(IRepository<Clothing> clothingRepository)
        {
            _clothingRepository = clothingRepository;
        }

        public async Task IsProductOwner(int productId, string userId)
        {
            var specification = new ClothingByUserIdSpecification(userId);
            var list = await _clothingRepository.Get(specification);
            var ids = list.Select(p => p.Id);
            if (!ids.Contains(productId))
            {
                throw new PermissionException();
            }
        }
    }
}
