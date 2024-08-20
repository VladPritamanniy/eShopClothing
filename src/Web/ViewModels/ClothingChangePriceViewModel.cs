using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels
{
    public class ClothingChangePriceViewModel
    {
        [Display(Name = "OldPrice")]
        public int? OldPrice { get; set; }

        [Required]
        [Display(Name = "ValidPrice")]
        public int ValidPrice { get; set; }
    }
}
