using CarParts.Models.Base;

namespace CarParts.Models.Main
{
    public class Move : BaseProp
    {
        public int InvoiceId { get; set; }
        public Invoice Invoice { get; set; }
        public int StoreCPId { get; set; }
        public StoreCP StoreCP { get; set; }
        public int Quantity { get; set; }
    }
}
