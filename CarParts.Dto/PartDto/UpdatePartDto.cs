using Microsoft.AspNetCore.Http;

namespace CarParts.Dto.PartDto
{
    public class UpdatePartDto
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<int> CarIds { get; set; }
        public int CategoryId { get; set; }
        public int BrandId { get; set; }
        public int StoreId { get; set; }
        public int OrginalPrice { get; set; }
        public int SellingPrice { get; set; }
        public IFormFile Image { get; set; }
        public int Quantity { get; set; }
    }
}
