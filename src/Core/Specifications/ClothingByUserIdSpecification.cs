using Core.Entities;
using Core.Specifications.Base;

namespace Core.Specifications
{
    public sealed class ClothingByUserIdSpecification : Specification<Clothing>
    {
        public ClothingByUserIdSpecification(string id)
        {
            AddCriteria(p => p.ApplicationUserId == id);
        }
    }
}
