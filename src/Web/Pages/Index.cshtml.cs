using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.Interfaces;
using Web.ViewModels;

namespace Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IClothingPageService _clothingPageService;

        [Required]
        public ClothingHomeIndexViewModel PageModel { get; set; } = new ClothingHomeIndexViewModel();

        public IndexModel(IClothingPageService clothingPageService)
        {
            _clothingPageService = clothingPageService;
        }

        public async Task OnGet(ClothingHomeIndexViewModel model, int pageNum = Constants.FIRST_PAGE_NUM)
        {
            PageModel = await _clothingPageService.GetPageItems(pageNum, Constants.ITEMS_PER_PAGE, model.TypeFilterApplied, model.SizeFilterApplied);
        }
    }
}
