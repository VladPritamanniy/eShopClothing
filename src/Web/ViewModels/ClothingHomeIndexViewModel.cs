using Microsoft.AspNetCore.Mvc.Rendering;

namespace Web.ViewModels
{
    public class ClothingHomeIndexViewModel
    {
        public List<ClothingItemPageViewModel> ClothingItems { get; set; } = new List<ClothingItemPageViewModel>();
        public List<SelectListItem>? Sizes { get; set; } = new List<SelectListItem>();
        public List<SelectListItem>? Types { get; set; } = new List<SelectListItem>();
        public int? SizeFilterApplied { get; set; }
        public int? TypeFilterApplied { get; set; }
        public PaginationInfoViewModel? PaginationInfo { get; set; }
        public int? BasketItemsCount { get; set; }
    }
}
