using Application.DTO;
using Application.Interfaces;
using AutoMapper;
using Core.Entities;
using Core.Repositories.Base;
using Core.Specifications;

namespace Application.Services
{
    public class BasketService : IBasketService
    {
        private readonly IRepository<Basket> _basketRepository;
        private readonly IRepository<Clothing> _clothingRepository;
        private readonly IMapper _mapper;

        public BasketService(IRepository<Basket> basketRepository, IMapper mapper, IRepository<Clothing> clothingRepository)
        {
            _basketRepository = basketRepository;
            _mapper = mapper;
            _clothingRepository = clothingRepository;
        }

        public async Task<BasketDto?> GetBasketByUserName(string userName)
        {
            var basketSpecification = new BasketWithItemsSpecification(userName);
            var entity = await _basketRepository.FirstOrDefaultAsync(basketSpecification);

            if (entity == null)
                return null;

            foreach (var item in entity.BasketItems)
            {
                var clothingSpec = new ClothingByIdSpecification(item.ClothingId);
                await _clothingRepository.FirstOrDefaultAsync(clothingSpec);
            }

            return _mapper.Map<BasketDto>(entity);
        }

        public async Task<BasketDto> CreateBasketForUser(string userName)
        {
            var basket = new Basket { BuyerId = userName};
            await _basketRepository.AddAsync(basket);
            return _mapper.Map<BasketDto>(basket);
        }

        public async Task<BasketDto> AddToBasket(string userName, int clothingId, int quantity = 1)
        {
            var basketSpec = new BasketWithItemsSpecification(userName);
            var basket = await _basketRepository.FirstOrDefaultAsync(basketSpec);

            if (basket == null)
            {
                basket = new Basket { BuyerId = userName };
                await _basketRepository.AddAsync(basket);
            }

            basket.BasketItems.Add(new BasketItem
            {
                ClothingId = clothingId,
                Quantity = quantity
            });

            await _basketRepository.UpdateAsync(basket);
            return _mapper.Map<BasketDto>(basket);
        }

        public async Task TransferBasket(string anonymousId, string userName)
        {
            var anonymousBasketSpec = new BasketWithItemsSpecification(anonymousId);
            var anonymousBasket = await _basketRepository.FirstOrDefaultAsync(anonymousBasketSpec);

            if (anonymousBasket == null) return;

            var userBasketSpec = new BasketWithItemsSpecification(userName);
            var userBasket = await _basketRepository.FirstOrDefaultAsync(userBasketSpec);

            if (userBasket == null)
            {
                userBasket = new Basket { BuyerId = userName };
                await _basketRepository.AddAsync(userBasket);
            }

            foreach (var item in anonymousBasket.BasketItems)
            {
                userBasket.BasketItems.Add(new BasketItem
                {
                    ClothingId = item.ClothingId,
                    Quantity = item.Quantity
                });
            }

            await _basketRepository.UpdateAsync(userBasket);
            await _basketRepository.DeleteAsync(anonymousBasket);
        }

        public async Task<int> GetCountProductsById(string userName)
        {
            var specification = new BasketWithItemsSpecification(userName);
            var entity = await _basketRepository.FirstOrDefaultAsync(specification);
            if (entity == null) return 0;
            return entity.BasketItems.Count;
        }
    }
}
