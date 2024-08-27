using Web.ViewModels.Base;

namespace Web.ViewModels
{
    public class ClothingItemPageViewModel : BaseViewModel
    {
        public string Name { get; set; }
        public PriceViewModel Price { get; set; }
        public DateTime CreationDate { get; set; }
        public string SizeName { get; set; }
        public byte[] Image { get; set; }
    }
}
