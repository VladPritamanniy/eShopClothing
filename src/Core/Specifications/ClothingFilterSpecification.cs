using Core.Entities;
using Core.Specifications.Base;

namespace Core.Specifications
{
    public class ClothingFilterSpecification : Specification<Clothing>
    {
        public ClothingFilterSpecification(int? typeId, int? sizeId)
        {
            AddCriteria(i => (!sizeId.HasValue || i.SizeId == sizeId) &&
                             (!typeId.HasValue || i.TypeId == typeId));
        }
    }
}
