using Core.Exceptions;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.Interfaces;
using Web.ViewModels;

namespace Web.Areas.Identity.Pages.Account.Manage
{
    [Authorize(Policy = "UserPolicy")]
    public class ChangeProductPriceModel : PageModel
    {
        private readonly ILogger<ChangeProductPriceModel> _logger;
        private readonly IChangeProductPricePageService _changeProductPricePageService;
        private readonly UserManager<ApplicationUser> _userManager;

        public ChangeProductPriceModel(ILogger<ChangeProductPriceModel> logger, IChangeProductPricePageService changeProductPricePageService, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _changeProductPricePageService = changeProductPricePageService;
            _userManager = userManager;
        }

        [BindProperty]
        public ClothingChangePriceViewModel Item { get; set; }

        public async Task<IActionResult> OnGet(int id, string? returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            try
            {
                var user = await _userManager.GetUserAsync(User);
                var userId = await _userManager.GetUserIdAsync(user!);
                var oldPrice = await _changeProductPricePageService.GetProductPrice(id, userId);
                Item = new ClothingChangePriceViewModel()
                {
                    OldPrice = oldPrice
                };
                return Page();
            }
            catch (PermissionException e)
            {
                _logger.LogError(e.Message);
                return LocalRedirect(returnUrl);
            }
            catch (ArgumentNullException e)
            {
                _logger.LogError(e.Message);
                return LocalRedirect(returnUrl);
            }
        }

        public async Task<IActionResult> OnPost(int id, string? returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            try
            {
                var user = await _userManager.GetUserAsync(User);
                var userId = await _userManager.GetUserIdAsync(user!);
                await _changeProductPricePageService.ChangePrice(id, Item.ValidPrice, userId);
                return LocalRedirect(returnUrl);
            }
            catch (PermissionException e)
            {
                _logger.LogError(e.Message);
                return LocalRedirect(returnUrl);
            }
            catch (ArgumentNullException e)
            {
                _logger.LogError(e.Message);
                return LocalRedirect(returnUrl);
            }
        }
    }
}
