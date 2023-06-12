using CarParts.Shared.Enums;

namespace CarParts.Dto.InvoiceDto
{
    public class GetAccountDto
    {
        public int Cost { get; set; }
        public string Description { get; set; }
        public InvoiceType InvoiceType { get; set; }
    }
}
