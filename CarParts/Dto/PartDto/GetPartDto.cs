using CarParts.Dto.CarPartsDto;
using CarParts.Dto.StoreCPDto;
using CarParts.Models.Main;

namespace CarParts.Dto.PartDto
{
    public class GetPartDto
    {
        public int TotalNumber { get; set; }
        public List<GetPartData> Parts { get; set; }
    }
}
