using CarParts.Dto.StoreCPDto;

namespace CarParts.Dto.CarPartsDto
{
    public class GetCarPartDto
    {
        public int Id { get; set; }
        public int PartId { get; set; }
        public int CarId { get; set; }
        public int BrandId { get; set; }
        public ICollection<GetStoreCPDto> StoreCPs { get; set; }
        public int OrginalPrice { get; set; }
        public int SellingPrice { get; set; }
        public string Image { get; set; }
    }
}
