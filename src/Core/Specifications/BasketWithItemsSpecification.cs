using Core.Entities;
using Core.Specifications.Base;

namespace Core.Specifications
{
    public class BasketWithItemsSpecification : Specification<Basket>
    {
        public BasketWithItemsSpecification(int basketId)
        {
            AddCriteria(p => p.Id == basketId);
            AddInclude(p=>p.BasketItems);
        }

        public BasketWithItemsSpecification(string buyerId)
        {
            AddCriteria(p=>p.BuyerId == buyerId);
            AddInclude(p => p.BasketItems);
        }
    }
}
