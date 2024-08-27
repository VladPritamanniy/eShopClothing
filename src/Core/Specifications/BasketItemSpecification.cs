using Core.Entities;
using Core.Specifications.Base;

namespace Core.Specifications
{
    public class BasketItemSpecification : Specification<BasketItem>
    {
        public BasketItemSpecification(int id)
        {
            AddCriteria(p=>p.Id == id);
        }
    }
}
