using System.ComponentModel.DataAnnotations;
using Web.ViewModels.Base;

namespace Web.ViewModels
{
    public class CreateClothingViewModel : BaseViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required] 
        public string Descriprion { get; set; }

        [Required]
        [Display(Name = "Select size")]
        public int SizeId { get; set; }

        [Required]
        [Display(Name = "Select type")]
        public int TypeId { get; set; }

        [Required]
        [Display(Name="Price")]
        public int ValidPrice { get; set; }

        [Required]
        [Display(Name = "File")]
        public List<IFormFile> FormFiles { get; set; }

        public HashSet<ImageViewModel> Images { get; set; } = new HashSet<ImageViewModel>();
    }
}
