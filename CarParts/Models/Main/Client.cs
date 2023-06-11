using CarParts.Models.Base;

namespace CarParts.Models.Main
{
    public class Client : BaseProp  
    {
        public string Name { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public bool IsSeller { get; set; }
        public ICollection<Invoice> Invoices { get; set; }
    }
}
