using CarParts.Models.Base;

namespace CarParts.Models.Main
{
    public class Car : BaseProp
    {
        public string Name { get; set; }
        public int Model { get; set; }
        public string Image { get; set; }
        public int BrandId { get; set; }
        public Brand Brand { get; set; }
        public int? CarCategoryId { get; set; }
        public CarCategory CarCategory { get; set; }
        public ICollection<CarPart> CarParts { get; set; }
    }
}
