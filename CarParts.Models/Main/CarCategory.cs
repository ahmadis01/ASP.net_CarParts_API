using CarParts.Models.Base;

namespace CarParts.Models.Main
{
    public class CarCategory : BaseProp
    {
        public string Name { get; set; }
        public ICollection<Car> Cars { get; set; }
    }
}
