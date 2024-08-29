using Application.DTO.Base;

namespace Application.DTO
{
    public class ClothingDto : BaseDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int SizeId { get; set; }
        public int TypeId { get; set; }
        public decimal? ValidPrice { get; set; }
        public decimal? OldPrice { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? DayOfSold { get; set; }
        public string ApplicationUserId { get; set; }
        public HashSet<ImageDto> Images { get; set; }
        public SizeDto Size { get; set; }
        public TypeDto Type { get; set; }
    }
}
