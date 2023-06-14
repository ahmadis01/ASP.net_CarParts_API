using CarParts.Models.Base;

namespace CarParts.Models.Main
{
    public class Store : BaseProp
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public ICollection<StorePart> StoreParts { get; set; }

    }
}
