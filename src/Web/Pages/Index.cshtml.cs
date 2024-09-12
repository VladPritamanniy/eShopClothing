using System.ComponentModel.DataAnnotations;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.Interfaces;
using Web.ViewModels;

namespace Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IClothingPageService _clothingPageService;
        private readonly IElasticService _elasticService;

        public IndexModel(IClothingPageService clothingPageService, IElasticService elasticService)
        {
            _clothingPageService = clothingPageService;
            _elasticService = elasticService;
        }

        [Required]
        public ClothingHomeIndexViewModel PageModel { get; set; } = new ClothingHomeIndexViewModel();

        public async Task OnGet(ClothingHomeIndexViewModel model, string searchString, int pageNum = Constants.FIRST_PAGE_NUM)
        {
            PageModel = await _clothingPageService.GetPageItems(pageNum, Constants.ITEMS_PER_PAGE, model.TypeFilterApplied, model.SizeFilterApplied, searchString);
        }

        public async Task<JsonResult> OnGetSuggestions(string term)
        {
            var result = await _elasticService.LiveSearch(term);
            return new JsonResult(result);
        }

        public Task OnPostSearch(string searchTerm)
        {
            return Task.CompletedTask;
        }
    }
}
