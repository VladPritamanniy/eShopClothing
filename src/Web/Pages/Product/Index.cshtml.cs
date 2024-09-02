using Application.Interfaces;
using Core.Exceptions.Subscription;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.Interfaces;
using Web.ViewModels;

namespace Web.Pages.Product
{
    public class IndexModel : PageModel
    {
        private readonly IClothingPageService _clothingPageService;
        private readonly ILogger<IndexModel> _logger;
        private readonly UserManager<ApplicationUser> _userManager;

        public IndexModel(IClothingPageService clothingPageService, ILogger<IndexModel> logger, UserManager<ApplicationUser> userManager)
        {
            _clothingPageService = clothingPageService;
            _logger = logger;
            _userManager = userManager;
        }

        public ClothingDetailsPageViewModel PageModel { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        [TempData]
        public string SuccessMessage { get; set; }

        public async Task<IActionResult> OnGet(int id)
        {
            try
            {
                var userId = _userManager.GetUserId(User);
                PageModel = await _clothingPageService.GetProductPageInfo(id, userId);
                return Page();
            }
            catch (ArgumentNullException e)
            {
                _logger.LogError(e.Message);
            }

            return LocalRedirect("/");
        }

        public async Task<IActionResult> OnPost(int id)
        {
            try
            {
                var userId = _userManager.GetUserId(User);
                await _clothingPageService.Subscribe(id, userId);
                SuccessMessage = "Subscription completed";
            }
            catch (SubscriptionNotAuthorizedExeption e)
            {
                _logger.LogError(e.Message);
                ErrorMessage = e.Message;
            }
            catch (SubscriptionExeption e)
            {
                _logger.LogError(e.Message);
                ErrorMessage = e.Message;
            }

            return LocalRedirect($"/product/{id}");
        }
    }
}
