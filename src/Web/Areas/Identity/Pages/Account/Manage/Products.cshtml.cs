using Application.Interfaces;
using AutoMapper;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.ViewModels;

namespace Web.Areas.Identity.Pages.Account.Manage
{
    [Authorize(Policy = "UserPolicy")]
    public class ProductsModel : PageModel
    {
        private readonly IClothingService _clothingService;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public ProductsModel(IClothingService clothingService, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _clothingService = clothingService;
            _mapper = mapper;
            _userManager = userManager;
        }

        public string ReturnUrl { get; set; }

        public IEnumerable<ClothingAccountViewModel> Items { get; set; }

        public async Task OnGet(string returnUrl = null)
        {
            returnUrl ??= Url.RouteUrl(string.Empty);

            var user = await _userManager.GetUserAsync(User);
            var userId = await _userManager.GetUserIdAsync(user!);

            var clothingDto = await _clothingService.GetAllUserProductByUserId(userId);

            Items = _mapper.Map<IEnumerable<ClothingAccountViewModel>>(clothingDto);
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPost(string returnUrl = null)
        {
            returnUrl ??= Url.RouteUrl(string.Empty);
            return LocalRedirect(returnUrl);
        }

        public async Task<IActionResult> OnPostDeleteProduct(int? id)
        {
            return LocalRedirect("/");
        }
    }
}
