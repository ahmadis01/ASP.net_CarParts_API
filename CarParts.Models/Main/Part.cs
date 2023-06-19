using CarParts.Models.Base;

namespace CarParts.Models.Main
{
    public class Part : BaseProp
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public int? CategoryId { get; set; }
        public Category Category { get; set; }
        public int BrandId { get; set; }
        public Brand Brand { get; set; }
        public int OrginalPrice { get; set; }
        public int SellingPrice { get; set; }
        public string Image { get; set; }
        public ICollection<StorePart> StoreParts { get; set; }
        public ICollection<CarPart> CarParts { get; set; }
    }
}
