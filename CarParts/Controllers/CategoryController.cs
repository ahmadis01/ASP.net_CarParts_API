using CarParts.Dto.CategoryDto;
using CarParts.Repoistory.CategoryRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarParts.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var categories = await categoryRepository.GetCategories();
            return Ok(categories);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategory(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var category = await categoryRepository.GetCategory(id);
            return Ok(category);
        }
        [HttpPost]
        public async Task<IActionResult> AddCategory([FromFormAttribute] AddCategoryDto categoryDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var category = await categoryRepository.AddCategory(categoryDto);
            return Ok(category);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateCategory(UpdateCategoryDto categoryDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var category = await categoryRepository.UpdateCategory(categoryDto);
            return Ok(category);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var result =  categoryRepository.DeleteCategory(id);
            return Ok(result);

        }
    }
}
