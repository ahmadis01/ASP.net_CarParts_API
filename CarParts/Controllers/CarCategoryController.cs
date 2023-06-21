using CarParts.Dto.CarCategoryDto;
using CarParts.Repositories.CarCategoryRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarParts.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarCategoryController : ControllerBase
    {
        private readonly ICarCategoryRepository _carCategoryRepository;
        public CarCategoryController(ICarCategoryRepository carCategoryRepository)
        {
            _carCategoryRepository = carCategoryRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetCarCategories()
        {
            var categories = await _carCategoryRepository.GetCarCategories();
            return Ok(categories);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCarCategory(int id)
        {
            var carCategory = await _carCategoryRepository.GetCarCategory(id);
            return Ok(carCategory);
        }
        [HttpPost]
        public async Task<IActionResult> AddCarCategory(AddCarCategoryDto dto)
        {
            var result = await _carCategoryRepository.AddCarCategory(dto);
            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCarCategory(int id)
        {
            var result = await _carCategoryRepository.DeleteCarCategory(id);
            return Ok(result);
        }
    }
}
