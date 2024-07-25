using Application.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.ViewModels;

namespace Web.Areas.Identity.Pages.Account.Manage
{
    public class ProductsModel : PageModel
    {
        public readonly IClothingService _clothingService;
        public IMapper _mapper;

        public ProductsModel(IClothingService clothingService, IMapper mapper)
        {
            _clothingService = clothingService;
            _mapper = mapper;
        }

        public string ReturnUrl { get; set; }

        public IEnumerable<CreateClothingViewModel> Items { get; set; }

        public async Task OnGet(string returnUrl = null)
        {
            returnUrl ??= Url.RouteUrl(string.Empty);
            
            var clothingDto = await _clothingService.GetAllUserProductByUserId("1");
            Items = _mapper.Map<IEnumerable<CreateClothingViewModel>>(clothingDto);
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPost(string returnUrl = null)
        {
            returnUrl ??= Url.RouteUrl(string.Empty);
            return LocalRedirect(returnUrl);
        }
    }
}
