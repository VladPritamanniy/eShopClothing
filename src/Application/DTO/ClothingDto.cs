using Application.DTO.Base;

namespace Application.DTO
{
    public class ClothingDto : BaseDto
    {
        public string Name { get; set; }
        public string Descriprion { get; set; }
        public int SizeId { get; set; }
        public int TypeId { get; set; }
        public int? ValidPrice { get; set; }
        public int? OldPrice { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? DayOfSold { get; set; }
        public HashSet<ImageDto> Images { get; set; }
        public SizeDto Size { get; set; }
        public TypeDto Type { get; set; }
    }
}
