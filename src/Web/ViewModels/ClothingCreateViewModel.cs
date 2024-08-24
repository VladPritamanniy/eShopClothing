using Microsoft.AspNetCore.Mvc.Rendering;
using Web.ViewModels.Base;

namespace Web.ViewModels
{
    public class ClothingCreateViewModel : BaseViewModel
    {
        public ClothingItemCreateViewModel Item { get; set; }
        public List<SelectListItem> Sizes { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> Types { get; set; } = new List<SelectListItem>();
    }
}
