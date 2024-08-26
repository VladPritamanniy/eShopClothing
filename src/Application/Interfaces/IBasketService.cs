using Application.DTO;

namespace Application.Interfaces
{
    public interface IBasketService
    {
        Task<BasketDto?> GetBasketByUserName(string userName);
        Task<BasketDto> CreateBasketForUser(string userName);
        Task<BasketDto> AddToBasket(string userName, int clothingId, int quantity = 1);
        Task TransferBasket(string anonymousId, string userName);
        Task<int> GetCountProductsById(string userName);
    }
}
