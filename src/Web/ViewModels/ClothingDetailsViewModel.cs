using Web.ViewModels.Base;

namespace Web.ViewModels
{
    public class ClothingDetailsViewModel : BaseViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public PriceViewModel Price { get; set; }
        public DateTime CreationDate { get; set; }
        public string SizeName { get; set; }
        public string TypeName { get; set; }
        public List<ImageViewModel> Images { get; set; } = new List<ImageViewModel>();
    }
}
