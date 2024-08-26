using Application.DTO.Base;

namespace Application.DTO
{
    public class BasketDto : BaseDto
    {
        public string BuyerId { get; set; }
        public List<BasketItemDto> BasketItems { get; set; } = new List<BasketItemDto>();
    }
}
