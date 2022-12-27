using CarParts.Dto.CarDto;
using CarParts.Models.Main;

namespace CarParts.Repoistory.CarRepository
{
    public interface ICarRepository
    {
        Task<IEnumerable<GetCarDto>> GetCars();
        Task<GetCarDto> GetCar(int id);
        Task<GetCarDto> GetCar(string name);
        Task<GetCarDto> AddCar(AddCarDto car);
        Task<GetCarDto> UpdateCar(UpdateCarDto car);
        bool DeleteCar(int id);


    }
}
