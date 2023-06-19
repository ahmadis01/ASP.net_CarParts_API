using CarParts.Models.Base;

namespace CarParts.Models.Main
{
    public class Move : BaseProp
    {
        public int InvoiceId { get; set; }
        public Invoice Invoice { get; set; }
        public int StorePartId { get; set; }
        public StorePart StorePart { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }

    }
}
