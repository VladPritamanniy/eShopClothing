using Application.DTO.Base;

namespace Application.DTO
{
    public class ImageDto : BaseDto
    {
        public byte[] Value { get; set; }
        public int ClothingId { get; set; }
    }
}
