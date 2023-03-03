using CarParts.Models.Base;

namespace CarParts.Models.Main
{
    public class Invoice : BaseProp 
    {
        public DateTime Date { get; set; }
        public bool IsImport { get; set; }
        public int Coast { get; set; }
        public string? Notes { get; set; }
        public int ClientId { get; set; }
        public Client Client { get; set; }
        public ICollection<Move> Moves { get; set; }

    }
}
