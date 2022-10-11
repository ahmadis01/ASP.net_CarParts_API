using CarParts.Models.Base;

namespace CarParts.Models.Main
{
    public class Invoice : BaseProp 
    {
        DateTime Date { get; set; }
        public bool IsImport { get; set; }
        public ICollection<Move> Moves { get; set; }

    }
}
