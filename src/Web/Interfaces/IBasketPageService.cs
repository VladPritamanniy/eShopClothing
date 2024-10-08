﻿using Web.ViewModels;

namespace Web.Interfaces
{
    public interface IBasketPageService
    {
        Task<BasketViewModel> GetOrCreateBasket(string userName);
        Task RemoveProductFromBasket(int basketItemId, string userName);
    }
}
