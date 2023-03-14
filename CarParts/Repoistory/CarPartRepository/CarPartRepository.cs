using AutoMapper;
using CarParts.Data;
using CarParts.Dto.BrandDto;
using CarParts.Dto.CarPartsDto;
using CarParts.Models.Main;
using CarParts.Parameters;
using Microsoft.EntityFrameworkCore;

namespace CarParts.Repoistory.CarPartRepository
{
    public class CarPartRepository 
    {
        /*  private readonly CarPartContext _context;
          private readonly IMapper _mapper;
          private readonly IWebHostEnvironment _environment;

          public CarPartRepository(CarPartContext context ,IMapper mapper , IWebHostEnvironment environment)
          {
              _context = context;
              _mapper = mapper;
              _environment = environment;
          }
          public async Task<IEnumerable<GetCarPartDto>> GetCarParts(CarPartParameters carPartParameters)
          {
              var carParts = await _context.CarParts.Include(c => c.Car).OrderBy(c => c.Id).ToListAsync();
              if (carPartParameters.PartId != 0)
                  carParts = carParts.Where(c => c.PartId == carPartParameters.PartId).ToList();

              if (carPartParameters.CarId != 0)
                  carParts = carParts.Where(c => c.CarId == carPartParameters.CarId).ToList();

              if (carPartParameters.CountryId != 0)
                  carParts = carParts.Where(c => c.Brand.CountryId == carPartParameters.CountryId).ToList();

              if (carPartParameters.BrandId != 0)
                  carParts = carParts.Where(c => c.BrandId == carPartParameters.BrandId).ToList();

              if (carPartParameters.StoreId != 0)
              {
                  // var storesCPs = await _context.StoreCPs.Where(s => s.StoreId == carPartParameters.StoreId).Include(s => s.CarPart).ToListAsync();
                  /*List<CarPart> parts = new List<CarPart>();
                  foreach (var carPart in carParts)
                  {
                      var part = carPart.StoreCPs.Where(s => s.StoreId == carPartParameters.StoreId).AsEnumerable();
                      parts.AddRange(part);
                  }
                  carParts = parts;
              }

              if (carPartParameters.IsOrginal)
                  carParts = carParts.ToList();

              var carPartsDto = _mapper.Map<List<GetCarPartDto>>(carParts);
              carPartsDto = carPartsDto.GroupBy(c => c.PartId ).Select(grp => grp.First()).ToList();
              return carPartsDto;
          }

          public async Task<GetCarPartDto> GetCarPart(int id)
          {
              var carPart = await _context.CarParts.FirstOrDefaultAsync(c => c.Id == id);
              var carPartDto = _mapper.Map<GetCarPartDto>(carPart);
              return carPartDto;
          }


          public async Task<List<GetCarPartDto>> AddCarPart(AddCarPartDto carPartDto)
          {

                  var carPart = _mapper.Map<CarPart>(carPartDto);
                  carPart.CarId = carPartDto.CarIds[i];
                  carPart.CreatedAt = DateTime.Now;
                  if(i == 0) {

                  }
                  else
                  {
                      carPart.Image = getCarParts[i - 1].Image;
                  }
                  var result = await _context.CarParts.AddAsync(carPart);
                  await _context.SaveChangesAsync();
                  var storeCP = new StorePart();
                  storeCP.CarPartId = result.Entity.Id;
                  storeCP.StoreId = carPartDto.StoreId;
                  storeCP.Quantity = carPartDto.Quantity;
                  var store = await _context.StoreCPs.AddAsync(storeCP);
                  getCarParts.Add(_mapper.Map<GetCarPartDto>(result.Entity)) ;     
              await _context.SaveChangesAsync();
              return getCarParts;
          }

          public async Task<GetCarPartDto> AddCarPartToNewStore(AddCarPartToNewStoreDto carPartDto)
          {
              var storeCP = new StorePart();
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
              var storeCP = new StorePart();
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
          }*/
    }
}
