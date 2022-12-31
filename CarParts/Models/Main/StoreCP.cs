using CarParts.Models.Base;

namespace CarParts.Models.Main
{
    public class StoreCP : BaseProp
    {
        public int CarPartId { get; set; }
        public CarPart CarPart { get; set; }
        public int StoreId { get; set; }
        public Store Store { get; set; }
        public int Quantity { get; set; }

    }
}
