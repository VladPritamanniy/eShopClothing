namespace Application.DTO
{
    public class ClothingPaginationFilterDto
    {
        public int PageNum { get; set; }
        public int PageSize { get; set; }
        public int? TypeId { get; set; }
        public int? SizeId { get; set; }
        public int[]? IdsArray { get; set; }
        public bool IsEnableSearching { get; set; }
    }
}
