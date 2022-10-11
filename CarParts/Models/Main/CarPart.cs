using CarParts.Models.Base;

namespace CarParts.Models.Main
{
    public class CarPart : BaseProp
    {
        public int PartId { get; set; }
        public Part Part { get; set; }
        public int CarId { get; set; }
        public Car Car { get; set; }
        public int BrandId { get; set; }
        public Brand Brand { get; set; }
        public int Quantity { get; set; }
        public int OrginalPrice { get; set; }
        public int SellingPrice { get; set; }
        public string Image { get; set; }
        public ICollection<StoreCP> StoreCPs { get; set; }
    }
}
