using Core.Entities.Base;

namespace Core.Entities
{
    public class Clothing : BaseEntity
    {
        public string Name { get; set; }
        public string Descriprion { get; set; }
        public int SizeId { get; set; }
        public int TypeId { get; set; }
        public int? ValidPrice { get; set; }
        public int? OldPrice { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? DayOfSold { get; set; }
        public Size Size { get; set; }
        public Type Type { get; set; }
    }
}
