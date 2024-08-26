using Core.Entities.Base;

namespace Core.Entities
{
    public class BasketItem : BaseEntity
    {
        public int Quantity { get; set; }
        public int ClothingId { get; set; }
        public int BasketId { get; set; }
        public Clothing Clothing { get; set; }
        public Basket Basket { get; set; }
    }
}
