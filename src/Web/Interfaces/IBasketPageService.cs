using Web.ViewModels;

namespace Web.Interfaces
{
    public interface IBasketPageService
    {
        Task<BasketViewModel> GetOrCreateBasket(string userName);
    }
}
