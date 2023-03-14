using CarParts.Dto.CarPartsDto;
using CarParts.Parameters;

namespace CarParts.Repoistory.CarPartRepository
{
    public interface ICarPartRepository
    {
        Task<IEnumerable<GetCarPartDto>> GetCarParts(PartParameters carPartParameters);
        Task<GetCarPartDto> GetCarPart(int id);
        Task<List<GetCarPartDto>> AddCarPart(AddCarPartDto carPartDto);
        Task<GetCarPartDto> AddCarPartToNewStore(AddCarPartToNewStoreDto carPartDto);
        Task<GetCarPartDto> UpdateCarPart(UpdateCarPartDto carPartDto);
        bool DeleteCarPart(int id);
    }
}
