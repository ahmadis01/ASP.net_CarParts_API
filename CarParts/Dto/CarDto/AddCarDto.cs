namespace CarParts.Dto.CarDto
{
    public class AddCarDto
    {
        public string Name { get; set; }
        public int Model { get; set; }
        public IFormFile Image { get; set; }
        public int BrandId { get; set; }
        public int? CarCategoryId { get; set; }
    }
}
