using CarParts.Models.Base;

namespace CarParts.Models.Main
{
    public class Brand : BaseProp
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public int CountryId { get; set; }
        public Country Country { get; set; }
        public ICollection<CarPart> CarParts { get; set; }
        public ICollection<Car> Car { get; set; }
    }
}
