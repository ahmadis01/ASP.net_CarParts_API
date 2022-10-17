using AutoMapper;
using CarParts.Data;
using CarParts.Dto;
using CarParts.Interfaces;
using CarParts.Models.Main;
using Microsoft.EntityFrameworkCore;

namespace CarParts.Repoistory
{
    public class BrandRepository : IBrandRepository
    {
        private readonly CarPartContext _context;
        private readonly IMapper _mapper;

        public BrandRepository(CarPartContext context ,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Brand> AddBrand(BrandDto brandDto)
        {
            var brand = _mapper.Map<Brand>(brandDto);
            var result = await _context.Brands.AddAsync(brand);
            return result.Entity;
        }

        public bool DeleteBrand(int id)
        {
            var brand = _context.Brands.FirstOrDefault(b => b.Id == id);
            _context.Remove(brand);
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public async Task<Brand> GetBrand(int id)
        {
            var brand = await _context.Brands.FirstOrDefaultAsync(b => b.Id == id);
            return brand;
        }

        public async Task<IEnumerable<Brand>> GetBrands()
        {
            var brands = await _context.Brands.OrderBy(b => b.Id).ToListAsync();
            return brands;
        }

        public async Task<Brand> UpdateBrand(BrandDto brandDto)
        {
            var brand = _mapper.Map<Brand>(brandDto);
            var result = _context.Brands.Update(brand);
            await _context.SaveChangesAsync();
            return result.Entity;
        }
    }
}
