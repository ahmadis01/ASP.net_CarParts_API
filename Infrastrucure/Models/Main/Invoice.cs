using CarParts.Models.Base;
using CarParts.Shared.Enums;

namespace CarParts.Models.Main
{
    public class Invoice : BaseProp 
    {
        public DateTime Date { get; set; }
        public bool IsImport { get; set; }
        public int Cost { get; set; }
        public string? Notes { get; set; }
        public int ClientId { get; set; }
        public Client Client { get; set; }
        public ICollection<Move> Moves { get; set; }
        public int Services { get; set; } = 0;
        public InvoiceType InvoiceType { get; set; }
    }
}
