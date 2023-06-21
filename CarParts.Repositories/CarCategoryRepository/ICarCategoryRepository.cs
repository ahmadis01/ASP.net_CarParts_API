using CarParts.Dto.CarCategoryDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarParts.Repositories.CarCategoryRepository
{
    public interface ICarCategoryRepository
    {
        Task<List<GetCarCategoryDto>> GetCarCategories();
        Task<GetCarCategoryDto> GetCarCategory(int id);
        Task<GetCarCategoryDto> AddCarCategory(AddCarCategoryDto dto);
        Task<bool> DeleteCarCategory(int id);
    }
}
