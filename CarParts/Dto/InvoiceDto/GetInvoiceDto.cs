namespace CarParts.Dto.InvoiceDto
{
    public class GetInvoiceDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int Cost { get; set; }
        public int ClientId { get; set; }
        public string? Notes { get; set; }
        public int Services { get; set; }
        public bool Received { get; set; }
    }
}
