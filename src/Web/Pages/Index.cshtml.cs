using System;
using System.ComponentModel.DataAnnotations;
using Application.Interfaces;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.Interfaces;
using Web.ViewModels;

namespace Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IClothingPageService _clothingPageService;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IBasketService _basketService;

        public IndexModel(IClothingPageService clothingPageService, SignInManager<ApplicationUser> signInManager, IBasketService basketService)
        {
            _clothingPageService = clothingPageService;
            _signInManager = signInManager;
            _basketService = basketService;
        }

        [Required]
        public ClothingHomeIndexViewModel PageModel { get; set; } = new ClothingHomeIndexViewModel();

        public async Task OnGet(ClothingHomeIndexViewModel model, int pageNum = Constants.FIRST_PAGE_NUM)
        {
            PageModel = await _clothingPageService.GetPageItems(pageNum, Constants.ITEMS_PER_PAGE, model.TypeFilterApplied, model.SizeFilterApplied);
            PageModel.BasketItemsCount = await CountTotalBasketItems();
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
