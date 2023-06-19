namespace CarParts.Dto.InvoiceDto
{
    public class AddPartsToInvoice
    {
        public int PartId { get; set; }
        public int StoreId { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
    }
}
