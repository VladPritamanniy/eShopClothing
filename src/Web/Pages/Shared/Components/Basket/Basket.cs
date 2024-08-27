using Application.Interfaces;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Web.ViewModels;

namespace Web.Pages.Shared.Components.Basket
{
    public class Basket : ViewComponent
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IBasketService _basketService;

        public Basket(SignInManager<ApplicationUser> signInManager, IBasketService basketService)
        {
            _signInManager = signInManager;
            _basketService = basketService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var vm = new BasketComponentViewModel
            {
                ItemsCount = await CountTotalBasketItems()
            };
            return View(vm);
        }

        private async Task<int> CountTotalBasketItems()
        {
            if (_signInManager.IsSignedIn(HttpContext.User))
            {
                return await _basketService.GetCountProductsById(User.Identity!.Name!);
            }

            string? anonymousId = GetAnonymousIdFromCookie();
            if (anonymousId == null)
                return 0;

            return await _basketService.GetCountProductsById(anonymousId);
        }

        private string? GetAnonymousIdFromCookie()
        {
            if (Request.Cookies.ContainsKey(Constants.BASKET_COOKIENAME))
            {
                var id = Request.Cookies[Constants.BASKET_COOKIENAME];

                if (Guid.TryParse(id, out _))
                {
                    return id;
                }
            }
            return null;
        }
    }
}
