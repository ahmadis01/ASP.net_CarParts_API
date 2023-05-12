using CarParts.Dto.PartDto;

namespace CarParts.Dto.CategoryDto
{
    public class GetCategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public int TotalParts { get; set; }
    }
}
