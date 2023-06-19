using Microsoft.AspNetCore.Http;

namespace CarParts.Dto.BrandDto
{
    public class UpdateBrandDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IFormFile Image { get; set; }
        public int CountryId { get; set; }
    }
}
