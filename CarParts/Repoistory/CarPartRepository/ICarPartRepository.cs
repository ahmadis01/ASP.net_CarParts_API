using CarParts.Dto.CarPartsDto;

namespace CarParts.Repoistory.CarPartRepository
{
    public interface ICarPartRepository
    {
        Task<IEnumerable<GetCarPartDto>> GetCarParts();
        Task<GetCarPartDto> GetCarPart(int id);
        Task<GetCarPartDto> AddCarPart(AddCarPartDto carPartDto);
        Task<GetCarPartDto> AddCarPartToNewStore(AddCarPartToNewStoreDto carPartDto);
        Task<GetCarPartDto> UpdateCarPart(UpdateCarPartDto carPartDto);
        bool DeleteCarPart(int id);
    }
}
