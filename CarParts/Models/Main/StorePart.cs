using CarParts.Models.Base;

namespace CarParts.Models.Main
{
    public class StorePart : BaseProp
    {
        public int PartId { get; set; }
        public Part Part { get; set; }
        public int StoreId { get; set; }
        public Store Store { get; set; }
        public int Quantity { get; set; }

    }
}
