using Core.Entities;
using Core.Specifications.Base;

namespace Core.Specifications
{
    public sealed class ClothingByIdSpecification : Specification<Clothing>
    {
        public ClothingByIdSpecification(int id)
        {
            AddCriteria(p => p.Id == id);
            AddInclude(p => p.Size);
            AddInclude(p => p.Type);
            AddInclude(p=>p.Images);
        }
    }
}
