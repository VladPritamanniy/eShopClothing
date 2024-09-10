using Application.DTO.Base;

namespace Application.DTO
{
    public class BasketItemDto : BaseDto
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal OldPrice { get; set; }
        public decimal ValidPrice { get; set; }
        public string ImagePublicId { get; set; }
        public string SizeName { get; set; }
    }
}
