using CarParts.Dto;
using CarParts.Models.Main;

namespace CarParts.Repoistory.CarRepository
{
    public interface ICarRepository
    {
        Task<IEnumerable<Car>> GetCars();
        Task<Car> GetCar(int id);
        Task<Car> GetCar(string name);
        Task<Car> AddCar(CarDto car);
        Task<Car> UpdateCar(CarDto car);
        bool DeleteCar(int id);


    }
}
