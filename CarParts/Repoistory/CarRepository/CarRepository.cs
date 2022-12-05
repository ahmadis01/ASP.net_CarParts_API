using AutoMapper;
using CarParts.Data;
using CarParts.Dto;
using CarParts.Models.Main;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace CarParts.Repoistory.CarRepository
{
    public class CarRepository : ICarRepository
    {
        private readonly CarPartContext _context;
        private readonly IMapper _mapper;

        public CarRepository(CarPartContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Car> AddCar(CarDto carDto)
        {
            Car car = _mapper.Map<Car>(carDto);
            car.CreatedAt = DateTime.Now;
            var result = await _context.Cars.AddAsync(car);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<IEnumerable<Car>> GetCars()
        {
            return await _context.Cars.ToListAsync();
        }
        public async Task<Car> GetCar(int id)
        {
            return await _context.Cars.FirstOrDefaultAsync(c => c.Id == id);
        }
        public async Task<Car> GetCar(string name)
        {
            return await _context.Cars.FirstOrDefaultAsync(c => c.Name == name);
        }

        public async Task<Car> UpdateCar(CarDto carDto)
        {
            var result = await _context.Cars.FirstOrDefaultAsync(c => c.Id == carDto.Id);
            if (result != null)
            {
                Car car = _mapper.Map<Car>(carDto);
                car.UpdatedAt = DateTime.Now;
                var re = _context.Cars.Update(car);
                await _context.SaveChangesAsync();
                return re.Entity;
            }
            return null;
        }

        public bool DeleteCar(int id)
        {
            var car = _context.Cars.FirstOrDefault(c => c.Id == id);
            _context.Remove(car);
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
