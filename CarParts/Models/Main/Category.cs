using CarParts.Models.Base;

namespace CarParts.Models.Main
{
    public class Category : BaseProp
    {
        public string Name { get; set; }
        public string? Image { get; set; }
        public ICollection<Part> Parts { get; set; }
    }
}
