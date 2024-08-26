using Web.ViewModels.Base;

namespace Web.ViewModels
{
    public class BasketItemViewModel : BaseViewModel
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal OldPrice { get; set; }
        public decimal ValidPrice { get; set; }
        public byte[] Image { get; set; }
        public string SizeName { get; set; }
    }
}
