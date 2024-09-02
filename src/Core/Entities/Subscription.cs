using Core.Entities.Base;

namespace Core.Entities
{
    public class Subscription : BaseEntity
    {
        public string UserId { get; set; }
        public string SellerId { get; set; }
        public DateTime SubscribedDate { get; set; }
    }
}
