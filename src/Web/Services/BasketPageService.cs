using Application.Interfaces;
using AutoMapper;
using Web.Interfaces;
using Web.ViewModels;

namespace Web.Services
{
    public class BasketPageService : IBasketPageService
    {
        private readonly IBasketService _basketService;
        private readonly IMapper _mapper;
        private readonly IPermissionService _permissionService;

        public BasketPageService(IBasketService basketService, IMapper mapper, IPermissionService permissionService)
        {
            _basketService = basketService;
            _mapper = mapper;
            _permissionService = permissionService;
        }

        public async Task<BasketViewModel> GetOrCreateBasket(string userName)
        {
            var basket = await _basketService.GetBasketByUserName(userName);

            if (basket == null)
                basket = await _basketService.CreateBasketForUser(userName);

            return _mapper.Map<BasketViewModel>(basket);
        }

        public async Task RemoveProductFromBasket(int basketItemId, string userName)
        {
            await _permissionService.IsBasketItemOwner(basketItemId, userName);
            await _basketService.DeleteBasketItemById(basketItemId);
        }
    }
}
