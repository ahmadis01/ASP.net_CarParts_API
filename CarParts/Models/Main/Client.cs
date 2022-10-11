using CarParts.Models.Base;

namespace CarParts.Models.Main
{
    public class Client : BaseProp  
    {
        public string Name { get; set; }
        public string? PhoneNumber { get; set; }
        public ICollection<Invoice> Invoices { get; set; }
    }
}
