using Core.Entities.Base;

namespace Core.Entities
{
    public class Image : BaseEntity
    {
        public byte[] Value { get; set; }
        public int ClothingId { get; set; }
    }
}
