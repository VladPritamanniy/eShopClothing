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

        public BasketPageService(IBasketService basketService, IMapper mapper)
        {
            _basketService = basketService;
            _mapper = mapper;
        }

        public async Task<BasketViewModel> GetOrCreateBasket(string userName)
        {
            var basket = await _basketService.GetBasketByUserName(userName);

            if (basket == null)
                basket = await _basketService.CreateBasketForUser(userName);

            return _mapper.Map<BasketViewModel>(basket);
        }
    }
}
