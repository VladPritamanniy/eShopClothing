using Web.ViewModels.Base;

namespace Web.ViewModels
{
    public class ClothingAccountViewModel : BaseViewModel
    {
        public string Name { get; set; }
        public int OldPrice { get; set; }
        public int ValidPrice { get; set; }
        public DateTime CreationDate { get; set; }
        public string SizeName { get; set; }
        public string TypeName { get; set; }
        public HashSet<ImageViewModel> Images { get; set; }
    }
}
