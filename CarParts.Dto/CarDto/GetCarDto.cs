using CarParts.Dto.BrandDto;
using CarParts.Dto.PartDto;

namespace CarParts.Dto.CarDto
{
    public class GetCarDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Model { get; set; }
        public string Image { get; set; }
        public int BrandId { get; set; }
        public int? CarCategoryId { get; set; }
        public int TotalParts { get; set; }
        public int? CountryId { get; set; }

    }
}
