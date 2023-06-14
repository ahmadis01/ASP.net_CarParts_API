using CarParts.Shared.Enums;

namespace CarParts.Dto.InvoiceDto
{
    public class UpdateInvoiceDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int Cost { get; set; }
        public string? Notes { get; set; }
        public int Services { get; set; }
        public InvoiceType InvoiceType { get; set; }
    }
}
