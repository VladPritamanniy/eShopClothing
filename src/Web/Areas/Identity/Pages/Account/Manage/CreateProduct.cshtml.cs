using Core.Exceptions.File;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web.Interfaces;
using Web.ViewModels;

namespace Web.Areas.Identity.Pages.Account.Manage
{
    [Authorize(Policy = "UserPolicy")]
    public class CreateProductModel : PageModel
    {
        private readonly IProductPageService _productPageService;

        public CreateProductModel(IProductPageService productPageService)
        {
            _productPageService = productPageService;
        }

        [BindProperty]
        public ClothingCreateViewModel Product { get; set; }

        public async Task OnGet(string? returnUrl = null)
        {
            var sizeList = await _productPageService.GetAllSizesClothing();
            var typeList = await _productPageService.GetAllTypesClothing();
            ViewData[nameof(ClothingCreateViewModel.SizeId)] = new SelectList(sizeList, nameof(SizeViewModel.Id), nameof(SizeViewModel.Name));
            ViewData[nameof(ClothingCreateViewModel.TypeId)] = new SelectList(typeList, nameof(TypeViewModel.Id), nameof(TypeViewModel.Name));
        }

        public async Task<IActionResult> OnPost(string? returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            try
            {
                await _productPageService.CreateProduct(Product, User);
                return LocalRedirect(returnUrl);
            }
            catch (FileSignatureException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return Page();
            }
            catch (FileFormatException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return Page();
            }
            catch (FileSizeException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return Page();
            }
            catch (ArgumentNullException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return Page();
            }
        }
    }
}
