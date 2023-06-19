using CarParts.Dto.StorePartDto;

namespace CarParts.Dto.PartDto
{
    public class GetPartData
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<int> Cars { get; set; }
        public List<GetStorePartDto> StoreParts { get; set; }
        public int CategoryId { get; set; }
        public int BrandId { get; set; }
        public int OrginalPrice { get; set; }
        public int SellingPrice { get; set; }
        public string Image { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
