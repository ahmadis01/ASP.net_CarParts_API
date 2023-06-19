using CarParts.Dto.BrandDto;
using CarParts.Models.Main;

namespace CarParts.Repoistory.BrandRepository
{
    public interface IBrandRepository
    {
        Task<IEnumerable<GetBrandDto>> GetBrands();
        Task<GetBrandDto> GetBrand(int id);
        Task<GetBrandDto> AddBrand(AddBrandDto brandDto);
        Task<GetBrandDto> UpdateBrand(UpdateBrandDto brandDto);
        bool DeleteBrand(int id);
    }
}
