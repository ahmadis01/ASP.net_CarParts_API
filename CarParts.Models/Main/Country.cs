using CarParts.Models.Base;

namespace CarParts.Models.Main
{
    public class Country : BaseProp
    {
        public string Name { get; set; }
        public ICollection<Brand> Brands { get; set; }
    }
}
