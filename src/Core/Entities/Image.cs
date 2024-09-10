using Core.Entities.Base;

namespace Core.Entities
{
    public class Image : BaseEntity
    {
        public string PublicId { get; set; }
        public int ClothingId { get; set; }
    }
}
