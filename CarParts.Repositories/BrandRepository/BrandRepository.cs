using AutoMapper;
using CarParts.SqlServer.DataBase;
using CarParts.Dto.BrandDto;
using CarParts.Models.Main;
using Microsoft.EntityFrameworkCore;
using System.IO;
using Microsoft.Extensions.Hosting;

namespace CarParts.Repoistory.BrandRepository
{
    public class BrandRepository : IBrandRepository
    {
        private readonly CarPartContext _context;
        private readonly IMapper _mapper;
        private readonly IHostEnvironment _environment;

        public BrandRepository(CarPartContext context, IMapper mapper, IHostEnvironment environment)
        {
            _context = context;
            _mapper = mapper;
            _environment = environment;
        }
        public async Task<GetBrandDto> AddBrand(AddBrandDto brandDto)
        {
            var brand = _mapper.Map<Brand>(brandDto);
            var fileName = DateTime.Now.Ticks.ToString();
            var extension = Path.GetExtension(brandDto.Image.FileName);
            var path = Path.Combine("wwwroot/Images/Brands",fileName + extension);
            brand.Image = Path.Combine("/Images/Brands", fileName + extension );
            brand.CreatedAt = DateTime.Now;
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                await brandDto.Image.CopyToAsync(stream);
                stream.Close();
            }
            var result = await _context.Brands.AddAsync(brand);
            await _context.SaveChangesAsync();
            var getBrand = _mapper.Map<GetBrandDto>(result.Entity);
            return getBrand;
        }

        public bool DeleteBrand(int id)
        {
            var brand = _context.Brands.FirstOrDefault(b => b.Id == id);
            var oldBrandPath = _environment.ContentRootPath.ToString() + "wwwroot" + brand.Image;
            if (File.Exists(oldBrandPath))
                File.Delete(oldBrandPath);
            _context.Remove(brand);
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public async Task<GetBrandDto> GetBrand(int id)
        {
            var brand = await _context.Brands.FirstOrDefaultAsync(b => b.Id == id);
            var brandDto = _mapper.Map < GetBrandDto >(brand);
            return brandDto;
        }

        public async Task<IEnumerable<GetBrandDto>> GetBrands()
        {
            var brands = await _context.Brands.OrderBy(b => b.Id).ToListAsync();
            var brandsDto = _mapper.Map<List<GetBrandDto>>(brands);
            return brandsDto;
        }

        public async Task<GetBrandDto> UpdateBrand(UpdateBrandDto brandDto)
        {
            var oldBrand = _context.Brands.AsNoTracking().FirstOrDefaultAsync(b => b.Id == brandDto.Id).Result;
            var oldBrandPath = _environment.ContentRootPath.ToString()+ "wwwroot"+ oldBrand.Image;
            if (File.Exists(oldBrandPath))
                File.Delete(oldBrandPath);
            var brand = _mapper.Map<Brand>(brandDto); 
            var fileName = DateTime.Now.Ticks.ToString();
            var extension = Path.GetExtension(brandDto.Image.FileName);
            var path = Path.Combine("wwwroot/Images/Brands", fileName + extension);
            brand.Image = Path.Combine("/Images/Brands", fileName + extension);
            brand.UpdatedAt = DateTime.Now;
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                await brandDto.Image.CopyToAsync(stream);
                stream.Close();
            }
            var result = _context.Brands.Update(brand);
            await _context.SaveChangesAsync();
            var getBrand = _mapper.Map<GetBrandDto>(result.Entity);
            return getBrand;
        }
    }
}
