using CarParts.Models.Base;

namespace CarParts.Models.Main
{
    public class Store : BaseProp
    {
        public string Location { get; set; }
        public ICollection<StoreCP> StoreCPs { get; set; }

    }
}
