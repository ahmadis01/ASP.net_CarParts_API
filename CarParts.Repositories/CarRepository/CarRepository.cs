using AutoMapper;
using CarParts.SqlServer.DataBase;
using CarParts.SharedKernal.Consts;
using CarParts.Dto.CarDto;
using CarParts.Models.Main;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using CarParts.Base.BaseRepository;

namespace CarParts.Repoistory.CarRepository
{
    public class CarRepository : BaseRepository, ICarRepository
    {
        private readonly CarPartContext _context;
        private readonly IMapper _mapper;
        private readonly IHostEnvironment _environment;

        public CarRepository(CarPartContext context, IMapper mapper, IHostEnvironment environment):base(environment)
        {
            _context = context;
            _mapper = mapper;
            _environment = environment;
        }

        public async Task<GetCarDto> AddCar(AddCarDto carDto)
        {
            var car = _mapper.Map<Car>(carDto);
            car.Image = await UploadFile(carDto.Image, FilePathConsts.CarFilePath);
            car.CreatedAt = DateTime.Now;
            var result = await _context.Cars.AddAsync(car);
            await _context.SaveChangesAsync();
            var getCar = _mapper.Map<GetCarDto>(result.Entity);
            return getCar;
        }

        public async Task<IEnumerable<GetCarDto>> GetCars()
        {
            var carIds = await _context.Cars.Select(c => c.Id).ToListAsync();
            var carParts = await _context.CarParts.Where(p => carIds.Contains(p.CarId)).Select(p => p.PartId).ToListAsync();
            var parts = await _context.Parts.Where(p => carParts.Contains(p.Id)).Select(p => p.Id).ToListAsync();
            var storeParts = await _context.StoreParts.Where(s => parts.Contains(s.PartId) && s.Quantity != 0).Select(s => s.PartId).ToListAsync();
            parts = storeParts.Distinct().ToList();
            carParts = parts.Distinct().ToList();
            var totalParts = carParts.Count;
            var cars = await _context.Cars.OrderBy(c => c.Id).Select(c => new GetCarDto
            {
                Id = c.Id,
                Name = c.Name,
                BrandId = c.BrandId,
                CarCategoryId = c.CarCategoryId,
                CountryId = c.Brand.CountryId,
                Image = c.Image,
                Model = c.Model,
                TotalParts = totalParts
            }).ToListAsync();
            return cars;
        }
        public async Task<GetCarDto> GetCar(int id)
        {
            var carParts = await _context.CarParts.Where(c => c.CarId == id).Select(c => c.PartId).ToListAsync();
            var parts = await _context.Parts.Where(p => carParts.Contains(p.Id)).Select(p => p.Id).ToListAsync();
            var storeParts = await _context.StoreParts.Where(s => parts.Contains(s.PartId) && s.Quantity != 0).Select(s => s.PartId).ToListAsync();
            parts = storeParts.Distinct().ToList();
            carParts = parts.Distinct().ToList();
            var totalCount = carParts.Count;
            var car = await _context.Cars.Select(c => new GetCarDto
            {
                Id = c.Id,
                BrandId = c.BrandId,
                CarCategoryId = c.CarCategoryId,
                CountryId = c.Brand.CountryId,
                Image = c.Image,
                Name = c.Name,
                Model = c.Model,
                TotalParts = totalCount
            }).FirstOrDefaultAsync(c => c.Id == id);
            return car;
        }
        public async Task<GetCarDto> GetCar(string name)
        {
            var car = await _context.Cars.Select(c => new GetCarDto
            {
                Id = c.Id,
                BrandId = c.BrandId,
                CarCategoryId = c.CarCategoryId,
                CountryId = c.Brand.CountryId,
                Image = c.Image,
                Name = c.Name,
                Model = c.Model,
                TotalParts = c.CarParts.Count()
            }).FirstOrDefaultAsync(c => c.Name.Contains(name));
            return car;
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
