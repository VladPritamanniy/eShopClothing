using Core.Entities;
using Core.Specifications.Base;

namespace Core.Specifications
{
    public class GetClothingListByUserIdSpecification : Specification<Clothing>
    {
        public GetClothingListByUserIdSpecification(string id) : base(p => p.ApplicationUserId == id)
        {
            AddInclude(p => p.Size);
            AddInclude(p => p.Type);
            AddInclude(p => p.Images);
            ApplyNoTracking();
        }
    }
}
