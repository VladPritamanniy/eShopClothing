namespace Application.DTO
{
    public class ClothingCreateDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int SizeId { get; set; }
        public int TypeId { get; set; }
        public decimal OldPrice { get; set; }
        public decimal ValidPrice { get; set; }
        public DateTime CreationDate { get; set; }
        public string ApplicationUserId { get; set; }
        public HashSet<ImageDto> Images { get; set; } = new HashSet<ImageDto>();
    }
}
