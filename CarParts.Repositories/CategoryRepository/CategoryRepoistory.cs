using AutoMapper;
using CarParts.SqlServer.DataBase;
using CarParts.Dto.CategoryDto;
using CarParts.Dto.PartDto;
using CarParts.Models.Main;
using Microsoft.EntityFrameworkCore;

namespace CarParts.Repoistory.CategoryRepository
{
    public class CategoryRepoistory : ICategoryRepository
    {
        private readonly CarPartContext context;
        private readonly IMapper mapper;

        public CategoryRepoistory(CarPartContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public async Task<GetCategoryDto> AddCategory(AddCategoryDto categoryDto)
        {
            var category = mapper.Map<Category>(categoryDto);
            var result = await context.AddAsync(category);
            await context.SaveChangesAsync();
            var getCategory = mapper.Map<GetCategoryDto>(result.Entity);
            return getCategory;
        }

        public bool DeleteCategory(int id)
        {
            var category = context.Categories.FirstOrDefault(c => c.Id == id);
            context.Remove(category);
            var saved = context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public async Task<IEnumerable<GetCategoryDto>> GetCategories()
        {
            var categories = await context.Categories.OrderBy(c => c.Id).ToListAsync();
            var categoriesDto = mapper.Map<List<GetCategoryDto>>(categories);
            return categoriesDto;
        }

        public async Task<GetCategoryDto> GetCategory(int id)
        {
            var category = await context.Categories.FirstOrDefaultAsync(c => c.Id == id);
            int totalParts = context.Parts.Where(p => p.CategoryId == category.Id).Count();
            var categoryDto = mapper.Map<GetCategoryDto>(category);
            categoryDto.TotalParts = totalParts;
            return categoryDto;
        }
        
        public async Task<GetCategoryDto> UpdateCategory(UpdateCategoryDto categoryDto)
        {
            var category = mapper.Map<Category>(categoryDto);
            category.UpdatedAt = DateTime.Now;
            var re = context.Categories.Update(category);
            await context.SaveChangesAsync();
            var getCategory = mapper.Map<GetCategoryDto>(re.Entity);
            return getCategory;
        }
    }
}
