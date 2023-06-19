using Microsoft.AspNetCore.Http;

namespace CarParts.Dto.CarDto
{
    public class UpdateCarDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Model { get; set; }
        public IFormFile Image { get; set; }
        public int BrandId { get; set; }
        public int? CarCategoryId { get; set; }
    }
}
