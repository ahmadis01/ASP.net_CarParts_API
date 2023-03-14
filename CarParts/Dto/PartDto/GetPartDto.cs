using CarParts.Dto.CarPartsDto;
using CarParts.Dto.StoreCPDto;
using CarParts.Models.Main;

namespace CarParts.Dto.PartDto
{
    public class GetPartDto
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<GetCarPartDto> CarParts { get; set; }
        public List<GetStorePartDto> StoreParts { get; set; }
        public int CategoryId { get; set; }
        public int BrandId { get; set; }
        public int OrginalPrice { get; set; }
        public int SellingPrice { get; set; }
        public string Image { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
