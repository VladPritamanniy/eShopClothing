using Core.Entities;
using Core.Specifications.Base;

namespace Core.Specifications
{
    public class SubscriptionSpecification : Specification<Subscription>
    {
        public SubscriptionSpecification(string userId, string sellerId)
        {
            AddCriteria(p => p.UserId == userId && p.SellerId == sellerId);
        }
    }
}
