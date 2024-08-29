using Application.DTO.Base;

namespace Application.DTO
{
    public class ClothingDetailsDto : BaseDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal ValidPrice { get; set; }
        public decimal OldPrice { get; set; }
        public DateTime CreationDate { get; set; }
        public string SizeName { get; set; }
        public string TypeName { get; set; }
        public List<ImageDto> Images { get; set; } = new List<ImageDto>();
    }
}
