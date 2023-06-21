using AutoMapper;
using CarParts.Dto.CarCategoryDto;
using CarParts.Models.Main;
using CarParts.SqlServer.DataBase;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarParts.Repositories.CarCategoryRepository
{
    public class CarCategoryRepository : ICarCategoryRepository
    {
        private readonly CarPartContext _context;
        private readonly IMapper _mapper;
        public CarCategoryRepository(CarPartContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<GetCarCategoryDto> AddCarCategory(AddCarCategoryDto dto)
        {
            var category = _mapper.Map<CarCategory>(dto);
            var result = await _context.CarCategories.AddAsync(category);
            await _context.SaveChangesAsync();
            var getCategory = _mapper.Map<GetCarCategoryDto>(result.Entity);
            return getCategory;
        }

        public async Task<bool> DeleteCarCategory(int id)
        {
            var category = _context.CarCategories.FirstOrDefaultAsync(c => c.Id == id);
            if(category != null)
                _context.CarCategories.Remove(category.Result);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<GetCarCategoryDto>> GetCarCategories()
        {
            var categories = await _context.CarCategories.OrderBy(c => c.Id).ToListAsync();
            var dto = _mapper.Map<List<GetCarCategoryDto>>(categories);
            return dto;
        }

        public async Task<GetCarCategoryDto> GetCarCategory(int id)
        {
            var entity = await _context.CarCategories.FirstOrDefaultAsync(c => c.Id == id);
            var category = _mapper.Map<GetCarCategoryDto>(entity);
            return category;
        }
    }
}
