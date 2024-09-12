using Core.Entities;
using Core.Specifications.Base;

namespace Core.Specifications
{
    public class ClothingFilterPaginatedSpecification : Specification<Clothing>
    {
        public ClothingFilterPaginatedSpecification(int skip, int take, int? typeId, int? sizeId, int[]? idsArray, bool isEnableSearching)
        {
            if (idsArray != null && isEnableSearching)
            {
                AddCriteria(p => idsArray.Contains(p.Id));
                ApplyOrderBy(p => Array.IndexOf(idsArray, p.Id));
            }
            else
            {
                AddCriteria(i => (!sizeId.HasValue || i.SizeId == sizeId) &&
                                 (!typeId.HasValue || i.TypeId == typeId));
                ApplyOrderByDescending(p => p.CreationDate);
            }

            if (skip > 1)
            {
                AddSkip((skip - 1) * take);
            }

            AddTake(take);
            AddInclude(p=>p.Images);
            AddInclude(p => p.Size);
            AddInclude(p => p.Type);
        }
    }
}
