namespace CarParts.Dto.InvoiceDto
{
    public class AddInvoiceDto
    {
        public DateTime Date { get; set; }
        public int Coast { get; set; } = 0;
        public bool IsImport { get; set; }
        public string? Notes { get; set; }
    }
}
