using Core.Entities.Base;

namespace Core.Entities
{
    public class Basket : BaseEntity
    {
        public string BuyerId { get; set; }
        public List<BasketItem> BasketItems { get; set; } = new List<BasketItem>();
    }
}
