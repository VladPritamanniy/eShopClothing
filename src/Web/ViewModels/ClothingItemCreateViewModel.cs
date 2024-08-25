using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels
{
    public class ClothingItemCreateViewModel
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
        [Display(Name = "Price")]
        public decimal Price { get; set; }

        [Required]
        [Display(Name = "File")]
        public List<IFormFile> FormFiles { get; set; }
    }
}
