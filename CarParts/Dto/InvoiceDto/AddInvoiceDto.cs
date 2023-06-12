using CarParts.Shared.Enums;
using Microsoft.AspNetCore.Mvc;

namespace CarParts.Dto.InvoiceDto
{
    public class AddInvoiceDto
    {
        public DateTime Date { get; set; }
        public int Cost { get; set; } = 0;
        public int ClientId { get; set; }
        public bool IsImport { get; set; }
        public string? Notes { get; set; }
        public int Services { get; set; }
        public InvoiceType InvoiceType { get; set; }
        public List<AddPartsToInvoice>? Parts { get; set; }
    }
}