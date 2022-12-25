using CarParts.Models.Base;

namespace CarParts.Models.Main
{
    public class Part : BaseProp
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public ICollection<CarPart> CarParts { get; set; }
    }
}
