using CarParts.Dto;
using CarParts.Models.Main;

namespace CarParts.Repoistory.CategoryRepository
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetCategories();
        Task<Category> GetCategory(int id);
        Task<Category> AddCategory(BrandDto brandDto);
        Task<Category> UpdateCategory(BrandDto brandDto);
        bool DeleteCategory(int id);
    }
}
