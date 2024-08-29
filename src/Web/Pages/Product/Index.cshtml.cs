using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.Interfaces;
using Web.ViewModels;

namespace Web.Pages.Product
{
    public class IndexModel : PageModel
    {
        private readonly IClothingPageService _clothingPageService;
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(IClothingPageService clothingPageService, ILogger<IndexModel> logger)
        {
            _clothingPageService = clothingPageService;
            _logger = logger;
        }

        public ClothingDetailsPageViewModel PageModel { get; set; }

        public async Task OnGet(int id)
        {
            try
            {
                PageModel = await _clothingPageService.GetProductPageInfo(id);
            }
            catch (ArgumentNullException e)
            {
                _logger.LogError(e.Message);
            }
        }
    }
}
