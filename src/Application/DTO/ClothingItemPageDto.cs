using Application.DTO.Base;

namespace Application.DTO
{
    public class ClothingItemPageDto : BaseDto
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public DateTime CreationDate { get; set; }
        public string SizeName { get; set; }
        public string TypeName { get; set; }
        public byte[] Image { get; set; }
    }
}
