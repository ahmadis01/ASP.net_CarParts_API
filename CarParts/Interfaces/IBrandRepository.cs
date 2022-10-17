using CarParts.Dto;
using CarParts.Models.Main;

namespace CarParts.Interfaces
{
    public interface IBrandRepository
    {
        Task<IEnumerable<Brand>> GetBrands();
        Task<Brand> GetBrand(int id);
        Task<Brand> AddBrand(BrandDto brandDto);
        Task<Brand> UpdateBrand(BrandDto brandDto);
        bool DeleteBrand(int id);
    }
}
