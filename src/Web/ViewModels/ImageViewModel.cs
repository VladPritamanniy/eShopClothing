using Web.ViewModels.Base;

namespace Web.ViewModels
{
    public class ImageViewModel : BaseViewModel
    {
        public byte[] Value { get; set; }
        public int ClothingId { get; set; }
    }
}
