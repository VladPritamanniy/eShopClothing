using Web.ViewModels.Base;

namespace Web.ViewModels
{
    public class AccountClothingViewModel : BaseViewModel
    {
        public string Name { get; set; }
        public int ValidPrice { get; set; }
        public DateTime CreationDate { get; set; }
        public HashSet<ImageViewModel> Images { get; set; }
        public SizeViewModel Size { get; set; }
        public TypeViewModel Type { get; set; }
    }
}
