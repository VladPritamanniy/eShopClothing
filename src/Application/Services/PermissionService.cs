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
            var specification = new ClothingIdsByOwnerIdSpecification(userId);
            var arrayIds = await _clothingRepository.ToArrayAsync(specification);
            if (arrayIds == null || arrayIds.Length == 0 && !arrayIds.Contains(productId))
            {
                throw new PermissionException();
            }
        }
    }
}
