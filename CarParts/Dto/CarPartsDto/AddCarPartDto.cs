using CarParts.Models.Main;

namespace CarParts.Dto.CarPartsDto
{
    public class AddCarPartDto
    {
        public int PartId { get; set; }
        public int CarId { get; set; }
        public int BrandId { get; set; }
        public int Quantity { get; set; }
        public int OrginalPrice { get; set; }
        public int SellingPrice { get; set; }
        public IFormFile Image { get; set; }
    }
}
