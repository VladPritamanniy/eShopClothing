using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels
{
    public class ChangeClothingPriceViewModel
    {
        [Display(Name = "OldPrice")]
        public int? OldPrice { get; set; }

        [Required]
        [Display(Name = "ValidPrice")]
        public int ValidPrice { get; set; }
    }
}
