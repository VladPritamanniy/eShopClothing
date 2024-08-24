using Core.Entities;
using Core.Specifications.Base;

namespace Core.Specifications
{
    public sealed class ClothingIncludeByUserIdSpecification : Specification<Clothing>
    {
        public ClothingIncludeByUserIdSpecification(string id)
        {
            AddCriteria(p => p.ApplicationUserId == id);
            ApplyOrderByDescending(p=>p.CreationDate);
            AddInclude(p => p.Size);
            AddInclude(p => p.Type);
            AddInclude(p => p.Images);
        }
    }
}
