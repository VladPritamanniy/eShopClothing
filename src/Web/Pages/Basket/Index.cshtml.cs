using Application.Interfaces;
using AutoMapper;
using Core.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.Interfaces;
using Web.ViewModels;

namespace Web.Pages.Basket
{
    public class IndexModel : PageModel
    {
        private readonly IBasketPageService _basketPageService;
        private readonly IBasketService _basketService;
        private readonly IClothingService _clothingService;
        private readonly ILogger<IndexModel> _logger;
        private readonly IMapper _mapper;

        public IndexModel(IBasketPageService basketPageService, IClothingService clothingService, ILogger<IndexModel> logger, IBasketService basketService, IMapper mapper)
        {
            _basketPageService = basketPageService;
            _clothingService = clothingService;
            _logger = logger;
            _basketService = basketService;
            _mapper = mapper;
        }

        public BasketViewModel PageModel { get; set; } = new BasketViewModel();

        [TempData]
        public string ErrorMessage { get; set; }

        [TempData]
        public string SuccessMessage { get; set; }

        public async Task<IActionResult> OnGet()
        {
            try
            {
                PageModel = await _basketPageService.GetOrCreateBasket(GetOrSetBasketCookie());
                return Page();
            }
            catch (ArgumentNullException e)
            {
                _logger.LogError(e.Message);
            }
            return LocalRedirect("/Index");
        }

        public async Task<IActionResult> OnPost(int productId)
        {
            if (productId <= 0)
                return LocalRedirect("/Index");
            try
            {
                var product = await _clothingService.GetById(productId);
                var username = GetOrSetBasketCookie();
                var basket = await _basketService.AddToBasket(username, product.Id);

                PageModel = _mapper.Map<BasketViewModel>(basket);
                SuccessMessage = "You added new product";
                return LocalRedirect("/Basket/Index");
            }
            catch (ArgumentNullException e)
            {
                _logger.LogError(e.Message);
            }

            ErrorMessage = "Error adding product to cart";
            return LocalRedirect("/Index");
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            if (id <= 0)
                return Page();
            try
            {
                var username = GetOrSetBasketCookie();
                await _basketPageService.RemoveProductFromBasket(id, username);
                SuccessMessage = "Product removed";
                return LocalRedirect("/Basket/Index");
            }
            catch (ArgumentNullException e)
            {
                _logger.LogError(e.Message);
            }
            catch (PermissionException e)
            {
                _logger.LogError(e.Message);
            }
            ErrorMessage = "Error removing product from cart";
            return Page();
        }

        private string GetOrSetBasketCookie()
        {
            if (Request.HttpContext.User.Identity == null)
                throw new ArgumentNullException();
            
            string? userName = null;

            if (Request.HttpContext.User.Identity.IsAuthenticated)
            {
                if (string.IsNullOrEmpty(Request.HttpContext.User.Identity.Name))
                    throw new ArgumentNullException();

                return Request.HttpContext.User.Identity.Name!;
            }

            if (Request.Cookies.ContainsKey(Constants.BASKET_COOKIENAME))
            {
                userName = Request.Cookies[Constants.BASKET_COOKIENAME];

                if (!Request.HttpContext.User.Identity.IsAuthenticated)
                {
                    if (!Guid.TryParse(userName, out var _))
                    {
                        userName = null;
                    }
                }
            }
            if (userName != null) return userName;
            
            userName = Guid.NewGuid().ToString();
            var cookieOptions = new CookieOptions { IsEssential = true, HttpOnly = true };
            cookieOptions.Expires = DateTime.Today.AddYears(1);
            Response.Cookies.Append(Constants.BASKET_COOKIENAME, userName, cookieOptions);

            return userName;
        }
    }
}
