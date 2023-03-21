using AutoMapper;
using CarParts.Data;
using CarParts.Dto.CarPartsDto;
using CarParts.Dto.PartDto;
using CarParts.Models.Main;
using CarParts.Parameters;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace CarParts.Repoistory.PartRepository
{
    public class PartRepository : IPartRepository
    {
        private readonly CarPartContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _environment;


        public PartRepository(CarPartContext context,IMapper mapper, IWebHostEnvironment environment)
        {
            _context = context;
            _mapper = mapper;
            _environment = environment;
        }
        public async Task<IEnumerable<GetPartDto>> GetParts(PartParameters parameters)
        {
            List<Part> parts;
            parts = await Pagination(parameters);
            parts = Filter(parameters, parts);

            if (!String.IsNullOrEmpty(parameters.Search.Trim()))
            {
                parts = parts.Where(p => p.Name.Contains(parameters.Search)).ToList();
            }

            var partsDto = _mapper.Map<List<GetPartDto>>(parts);
            return partsDto;
        }
        public async Task<GetPartDto> AddPart(AddPartDto partDto)
        {
            var part = _mapper.Map<Part>(partDto);            
            part.CreatedAt = DateTime.Now;
            //upload image
            var fileName = DateTime.Now.Ticks.ToString();
            var extension = Path.GetExtension(partDto.Image.FileName);
            var path = Path.Combine("wwwroot/Images/Parts", fileName + extension);
            part.Image = Path.Combine("/Images/Parts", fileName + extension);
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                await partDto.Image.CopyToAsync(stream);
                stream.Close();
            }
            var partRes = await _context.Parts.AddAsync(part);
            await _context.SaveChangesAsync();
            //connect the part with cars
            for (int i = 0; i < partDto.CarIds.Count; i++)
            {
                var carPart = new CarPart();
                carPart.CarId = i + 1;
                carPart.PartId = partRes.Entity.Id;
                await _context.CarParts.AddAsync(carPart);
                await _context.SaveChangesAsync();
            }
            //connect the part with store
            var storePart = new StorePart();
            storePart.PartId = partRes.Entity.Id;
            storePart.StoreId = partDto.StoreId;
            storePart.Quantity = partDto.Quantity;

            await _context.StoreParts.AddAsync(storePart);
            await _context.SaveChangesAsync();
            var getPart = _mapper.Map<GetPartDto>(part);
            return getPart;
        }

        public bool DeletePart(int id)
        {
            var part = _context.Parts.FirstOrDefault(p => p.Id == id);
            var oldPartPath = _environment.ContentRootPath.ToString() + "wwwroot" + part.Image;
            if (File.Exists(oldPartPath))
                File.Delete(oldPartPath);
            _context.Parts.Remove(part);
            var result = _context.SaveChanges();
            return result > 0 ? true : false;
        }
        public async Task<GetPartDto> GetPart(int id)
        {
            var part = await _context.Parts.Include(p => p.CarParts)
                .Include(p => p.StoreParts)
                .FirstOrDefaultAsync(c => c.Id == id);
            var partDto = _mapper.Map<GetPartDto>(part);
            return partDto;       
        }

        public async Task<List<GetPartDto>> GetPart(string name)
        {
            var part = await _context.Parts.Include(p => p.CarParts)
                .Include(p => p.StoreParts)
                .Where(p => p.Name.Contains(name))
                .ToListAsync();
            var partDto = _mapper.Map<List<GetPartDto>>(part);
            return partDto;
        }
        public async Task<GetPartDto> UpdatePart(UpdatePartDto partDto)
        {
            var oldPart = _context.Parts.AsNoTracking().FirstOrDefaultAsync(b => b.Id == partDto.Id).Result;
            var oldPartDto = _environment.ContentRootPath.ToString() + "wwwroot" + oldPart.Image;
            if (File.Exists(oldPartDto))
                File.Delete(oldPartDto);
            var part = _mapper.Map<Part>(partDto);
            var fileName = DateTime.Now.Ticks.ToString();
            var extension = Path.GetExtension(partDto.Image.FileName);
            var path = Path.Combine("wwwroot/Images/Parts", fileName + extension);
            part.Image = Path.Combine("/Images/Parts", fileName + extension);
            part.UpdatedAt = DateTime.Now;
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                await partDto.Image.CopyToAsync(stream);
                stream.Close();
            }
            var result = _context.Parts.Update(part);
            await _context.SaveChangesAsync();
            var getPart = _mapper.Map<GetPartDto>(result.Entity);
            return getPart;
        }
        public List<Part> Filter(PartParameters parameters , List<Part> parts)
        {

            if (parameters.CountryId != 0)
            {
                parts = parts.Where(p => p.Brand.CountryId == parameters.CountryId).ToList();
            }
            if (parameters.BrandId != 0)
            {
                parts = parts.Where(p => p.BrandId == parameters.BrandId).ToList();
            }
            if (parameters.StoreId != 0)
            {
                var stores = _context.StoreParts.Where(s => s.StoreId == parameters.StoreId).ToList();
                foreach (var store in stores)
                {
                    parts = parts.Where(p => p.Id == store.PartId).ToList();
                }
            }
            if (!String.IsNullOrEmpty(parameters.OrderBy))
            {
                switch (parameters.OrderBy)
                {
                    case "name":
                        parts = parts.OrderBy(p => p.Name).ToList();
                        break;
                    case "price":
                        parts = parts.OrderBy(p => p.SellingPrice).ToList();
                        break;
                    case "date":
                        parts = parts.OrderBy(p => p.CreatedAt).ToList();
                        break;
                }
            }
            if(parameters.OrderStatus.ToLower() == "desc")
            {
                parts.Reverse();
            }
            return parts;
        }
        public async Task<List<Part>> Pagination(PartParameters parameters)
        {
            var parts = new List<Part>();
            if (parameters.PageSize != 0 && parameters.PageNumber != 0)
            {
                parts = await _context.Parts
                    .OrderBy(p => p.Id)
                    .Include(p => p.CarParts).Include(p => p.StoreParts)
                    .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                    .Take(parameters.PageSize)
                    .ToListAsync();
            }
            else
            {
                parts = await _context.Parts
                    .OrderBy(p => p.Id)
                    .Include(p => p.CarParts).Include(p => p.StoreParts)
                    .ToListAsync();
            }
            return parts;
        }
    }
}
