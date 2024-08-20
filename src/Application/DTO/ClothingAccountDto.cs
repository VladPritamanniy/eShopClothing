using Application.DTO.Base;

namespace Application.DTO
{
    public class ClothingAccountDto : BaseDto
    {
        public string Name { get; set; }
        public int OldPrice { get; set; }
        public int ValidPrice { get; set; }
        public DateTime CreationDate { get; set; }
        public HashSet<ImageDto> Images { get; set; }
        public SizeDto Size { get; set; }
        public TypeDto Type { get; set; }
    }
}
