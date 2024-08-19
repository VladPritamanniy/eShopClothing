using Core.Entities;
using Core.Specifications.Base;

namespace Core.Specifications
{
    public sealed class ClothingIdsByOwnerIdSpecification : Specification<Clothing, int>
    {
        public ClothingIdsByOwnerIdSpecification(string id)
        {
            ApplySelector(p => p.Id);
            AddCriteria(p => p.ApplicationUserId == id);
        }
    }
}
