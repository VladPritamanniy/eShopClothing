using Core.Entities;
using Core.Specifications.Base;

namespace Core.Specifications
{
    public class ClothingFilterPaginatedSpecification : Specification<Clothing>
    {
        public ClothingFilterPaginatedSpecification(int skip, int take, int? typeId, int? sizeId)
        {
            AddCriteria(i => (!sizeId.HasValue || i.SizeId == sizeId) &&
                             (!typeId.HasValue || i.TypeId == typeId));
            if (skip > 1)
            {
                AddSkip((skip - 1) * take);
            }

            AddTake(take);
            ApplyOrderByDescending(p=>p.CreationDate);
            AddInclude(p=>p.Images);
            AddInclude(p => p.Size);
            AddInclude(p => p.Type);
        }
    }
}
