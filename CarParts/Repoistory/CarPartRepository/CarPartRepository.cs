using AutoMapper;
using CarParts.Data;
using CarParts.Dto.BrandDto;
using CarParts.Dto.CarPartsDto;
using CarParts.Models.Main;
using Microsoft.EntityFrameworkCore;

namespace CarParts.Repoistory.CarPartRepository
{
    public class CarPartRepository : ICarPartRepository
    {
        private readonly CarPartContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _environment;

        public CarPartRepository(CarPartContext context ,IMapper mapper , IWebHostEnvironment environment)
        {
            _context = context;
            _mapper = mapper;
            _environment = environment;
        }
        public async Task<IEnumerable<GetCarPartDto>> GetCarParts()
        {
            var carParts = await _context.CarParts.Include(c => c.StoreCPs).OrderBy(c => c.Id).ToListAsync();
            var carPartsDto = _mapper.Map<List<GetCarPartDto>>(carParts);
            return carPartsDto;
        }

        public async Task<GetCarPartDto> GetCarPart(int id)
        {
            var carPart = await _context.CarParts.Include(c => c.StoreCPs).FirstOrDefaultAsync(c => c.Id == id);
            var carPartDto = _mapper.Map<GetCarPartDto>(carPart);
            return carPartDto;
        }


        public async Task<GetCarPartDto> AddCarPart(AddCarPartDto carPartDto)
        {
            var carPart = _mapper.Map<CarPart>(carPartDto);
            var fileName = DateTime.Now.Ticks.ToString();
            var extension = Path.GetExtension(carPartDto.Image.FileName);
            var path = Path.Combine("wwwroot/Images/CarParts", fileName + extension);
            carPart.Image = Path.Combine("/Images/CarParts", fileName + extension);
            carPart.CreatedAt = DateTime.Now;
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                await carPartDto.Image.CopyToAsync(stream);
                stream.Close();
            }
            var result = await _context.CarParts.AddAsync(carPart);
            await _context.SaveChangesAsync();
            var storeCP = new StoreCP();
            storeCP.CarPartId = result.Entity.Id;
            storeCP.StoreId = carPartDto.StoreId;
            storeCP.Quantity = carPartDto.Quantity;
            var store = await _context.StoreCPs.AddAsync(storeCP);
            await _context.SaveChangesAsync();
            var getCarPart = _mapper.Map<GetCarPartDto>(result.Entity);
            return getCarPart;
        }

        public async Task<GetCarPartDto> AddCarPartToNewStore(AddCarPartToNewStoreDto carPartDto)
        {
            var storeCP = new StoreCP();
            storeCP.CarPartId = carPartDto.CarPartId;
            storeCP.StoreId = carPartDto.StoreId;
            storeCP.Quantity = carPartDto.Quantity;
            var store = await _context.StoreCPs.AddAsync(storeCP);
            await _context.SaveChangesAsync();
            var carPart = await GetCarPart(carPartDto.CarPartId);
            var getCarPart = _mapper.Map<GetCarPartDto>(carPart);
            return getCarPart;
        }

        public async Task<GetCarPartDto> UpdateCarPart(UpdateCarPartDto carPartDto)
        {
            var oldCarPart = _context.CarParts.AsNoTracking().FirstOrDefaultAsync(b => b.Id == carPartDto.Id).Result;
            var oldCarPartDto = _environment.ContentRootPath.ToString() + "wwwroot" + oldCarPart.Image;
            if (File.Exists(oldCarPartDto))
                File.Delete(oldCarPartDto);
            var carPart = _mapper.Map<CarPart>(carPartDto);
            var fileName = DateTime.Now.Ticks.ToString();
            var extension = Path.GetExtension(carPartDto.Image.FileName);
            var path = Path.Combine("wwwroot/Images/CarParts", fileName + extension);
            carPart.Image = Path.Combine("/Images/CarParts", fileName + extension);
            carPart.UpdatedAt = DateTime.Now;
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                await carPartDto.Image.CopyToAsync(stream);
                stream.Close();
            }
            var result = _context.CarParts.Update(carPart);
            await _context.SaveChangesAsync();
            var storeCP = new StoreCP();
            storeCP.CarPartId = result.Entity.Id;
            storeCP.StoreId = carPartDto.StoreId;
            storeCP.Quantity = carPartDto.Quantity;
            var store = _context.StoreCPs.Update(storeCP);
            await _context.SaveChangesAsync();
            var getCarPart = _mapper.Map<GetCarPartDto>(result.Entity);
            return getCarPart;
        }

        public bool DeleteCarPart(int id)
        {
            var carPart = _context.CarParts.FirstOrDefault(b => b.Id == id);
            var oldCarPartPath = _environment.ContentRootPath.ToString() + "wwwroot" + carPart.Image;
            if (File.Exists(oldCarPartPath))
                File.Delete(oldCarPartPath);
            _context.Remove(carPart);
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
