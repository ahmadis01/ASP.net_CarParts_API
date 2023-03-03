namespace CarParts.Dto.InvoiceDto
{
    public class GetInvoiceDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int Coast { get; set; }
        public string? Notes { get; set; }
    }
}
