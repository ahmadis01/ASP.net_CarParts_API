using CarParts.Models.Base;

namespace CarParts.Models.Main
{
    public class CarPart : BaseProp
    {
        public int PartId { get; set; }
        public Part Part { get; set; }
        public int CarId { get; set; }
        public Car Car { get; set; }
        
    }
}
