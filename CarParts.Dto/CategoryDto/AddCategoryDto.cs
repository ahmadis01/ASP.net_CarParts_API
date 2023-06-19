using Microsoft.AspNetCore.Http;

namespace CarParts.Dto.CategoryDto
{
    public class AddCategoryDto
    {
        public string Name { get; set; }
        public IFormFile Image { get; set; }
    }
}
