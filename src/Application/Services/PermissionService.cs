using Application.Interfaces;
using Core.Entities;
using Core.Exceptions.Permission;
using Core.Repositories.Base;
using Core.Specifications;

namespace Application.Services
{
    public class PermissionService : IPermissionService
    {
        private readonly IRepository<Clothing> _clothingRepository;
        private readonly IRepository<Basket> _basketRepository;
        private readonly IRepository<BasketItem> _basketItemRepository;

        public PermissionService(IRepository<Clothing> clothingRepository, IRepository<Basket> basketRepository, IRepository<BasketItem> basketItemRepository)
        {
            _clothingRepository = clothingRepository;
            _basketRepository = basketRepository;
            _basketItemRepository = basketItemRepository;
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

        public async Task<int> IsBasketItemOwner(int basketItemId, string userName)
        {
            var basketSpecification = new BasketWithItemsSpecification(userName);
            var basket = await _basketRepository.FirstOrDefaultAsync(basketSpecification);
            if (basket == null)
                throw new ArgumentNullException($"Cannot get basket by id = {userName}");

            var basketItemSpecification = new BasketItemSpecification(basketItemId);
            var basketItem = await _basketItemRepository.FirstOrDefaultAsync(basketItemSpecification);
            if (basketItem == null)
                throw new PermissionException();

            return basketItem.Id;
        }
    }
}
