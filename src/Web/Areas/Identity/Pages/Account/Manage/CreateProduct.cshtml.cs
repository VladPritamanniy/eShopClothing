using Core.Exceptions.AV;
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
        private readonly ILogger<CreateProductModel> _logger;

        public CreateProductModel(IProductPageService productPageService, ILogger<CreateProductModel> logger)
        {
            _productPageService = productPageService;
            _logger = logger;
        }

        [BindProperty]
        public ClothingCreateViewModel PageModel { get; set; } = new ClothingCreateViewModel();

        public async Task OnGet(string? returnUrl = null)
        {
            await InitializePageModel();
        }

        public async Task<IActionResult> OnPost(string? returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                await InitializePageModel();
                return Page();
            }

            returnUrl ??= Url.Content("~/");

            try
            {
                await _productPageService.CreateProductAndNotifySubscribers(PageModel.Item, User);
                return LocalRedirect(returnUrl);
            }
            catch (FileSignatureException ex)
            {
                _logger.LogError(ex.Message);
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            catch (FileFormatException ex)
            {
                _logger.LogError(ex.Message);
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            catch (FileSizeException ex)
            {
                _logger.LogError(ex.Message);
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            catch (ArgumentNullException ex)
            {
                _logger.LogError(ex.Message);
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            catch (InfectedFileException ex)
            {
                _logger.LogError(ex.Message);
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            await InitializePageModel();
            return Page();
        }

        private async Task InitializePageModel()
        {
            var sizeList = await _productPageService.GetAllSizesClothing();
            var typeList = await _productPageService.GetAllTypesClothing();

            PageModel.Sizes = sizeList
                .Select(type => new SelectListItem() { Value = type.Id.ToString(), Text = type.Name })
                .OrderBy(t => t.Value)
                .ToList();

            PageModel.Types = typeList
                .Select(type => new SelectListItem() { Value = type.Id.ToString(), Text = type.Name })
                .OrderBy(t => t.Text)
                .ToList();
        }
    }
}
