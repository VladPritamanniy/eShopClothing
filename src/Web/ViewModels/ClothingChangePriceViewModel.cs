using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels
{
    public class ClothingChangePriceViewModel
    {
        [Display(Name = "OldPrice")]
        public decimal? OldPrice { get; set; }

        [Required]
        [Display(Name = "ValidPrice")]
        public decimal ValidPrice { get; set; }
    }
}
