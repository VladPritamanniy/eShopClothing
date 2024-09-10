using Application.DTO.Base;

namespace Application.DTO
{
    public class ImageDto : BaseDto
    {
        public string PublicId { get; set; }
        public int ClothingId { get; set; }
    }
}
