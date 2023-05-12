using AutoMapper;
using CarParts.Data;
using CarParts.Dto.CarDto;
using CarParts.Dto.PartDto;
using CarParts.Models.Main;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace CarParts.Repoistory.CarRepository
{
    public class CarRepository : ICarRepository
    {
        private readonly CarPartContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _environment;

        public CarRepository(CarPartContext context, IMapper mapper,IWebHostEnvironment environment)
        {
            _context = context;
            _mapper = mapper;
            _environment = environment;
        }

        public async Task<GetCarDto> AddCar(AddCarDto carDto)
        {
            var car = _mapper.Map<Car>(carDto);
            var extension = Path.GetExtension(carDto.Image.FileName);
            var fileName = DateTime.Now.Ticks.ToString();
            var path = Path.Combine("wwwroot/Images/Cars", fileName+ extension);
            car.Image = Path.Combine("/Images/Cars", fileName+ extension);
            car.CreatedAt = DateTime.Now;
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                await carDto.Image.CopyToAsync(stream);
                stream.Close();
            }
            var result = await _context.Cars.AddAsync(car);
            await _context.SaveChangesAsync();
            var getCar = _mapper.Map<GetCarDto>(result.Entity);
            return getCar;
        }

        public async Task<IEnumerable<GetCarDto>> GetCars()
        {
            var cars = await _context.Cars.OrderBy(a => a.Id).ToListAsync();
            var carsDto = _mapper.Map<List<GetCarDto>>(cars);
            return carsDto;
        }
        public async Task<GetCarDto> GetCar(int id)
        {
            var car = await _context.Cars.FirstOrDefaultAsync(c => c.Id == id);
            int TotalParts = _context.CarParts.Where(c => c.CarId == id).Count();
            var carDto = _mapper.Map<GetCarDto>(car);
            carDto.TotalParts = TotalParts;
            return carDto;
        }
        public async Task<GetCarDto> GetCar(string name)
        {
            var car =  await _context.Cars.FirstOrDefaultAsync(c => c.Name == name);
            var carDto = _mapper.Map<GetCarDto>(car);
            return carDto;
        }

        public async Task<GetCarDto> UpdateCar(UpdateCarDto carDto)
        {
            Car car = _mapper.Map<Car>(carDto);
            var oldCar = _context.Cars.AsNoTracking().FirstOrDefaultAsync(c => c.Id == carDto.Id).Result;
            var oldCarPath =_environment.ContentRootPath.ToString()+"wwwroot"+ oldCar.Image;
            if(File.Exists(oldCarPath))
                File.Delete(oldCarPath);
            var extension = Path.GetExtension(carDto.Image.FileName);
            var fileName = DateTime.Now.Ticks.ToString();
            var path = Path.Combine("wwwroot/Images/Cars",fileName+extension);
            car.Image = Path.Combine("/Images/Cars" , fileName+extension);
            car.UpdatedAt = DateTime.Now;
            using(FileStream stream = new FileStream(path, FileMode.Create))
            {
                await carDto.Image.CopyToAsync(stream);
                stream.Close();
            }
            var re = _context.Cars.Update(car);
            await _context.SaveChangesAsync();
            var getCar = _mapper.Map<GetCarDto>(re.Entity);
            return getCar;
        }

        public bool DeleteCar(int id)
        {
            var car = _context.Cars.FirstOrDefault(c => c.Id == id);
            var oldCarPath = _environment.ContentRootPath.ToString() + "wwwroot" + car.Image;
            if (File.Exists(oldCarPath))
                File.Delete(oldCarPath);
            _context.Remove(car);
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
