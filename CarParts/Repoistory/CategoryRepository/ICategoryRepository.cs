using CarParts.Dto.CategoryDto;
using CarParts.Models.Main;

namespace CarParts.Repoistory.CategoryRepository
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<GetCategoryDto>> GetCategories();
        Task<GetCategoryDto> GetCategory(int id);
        Task<GetCategoryDto> AddCategory(AddCategoryDto categoryDto);
        Task<GetCategoryDto> UpdateCategory(UpdateCategoryDto categoryDto);
        bool DeleteCategory(int id);
    }
}
