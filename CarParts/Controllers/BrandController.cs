using CarParts.Dto;
using CarParts.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarParts.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly IBrandRepository _brandRepository;

        public BrandController(IBrandRepository brandRepository)
        {
            _brandRepository = brandRepository;
        }
        [HttpGet]
        public async Task<ActionResult> GetBrands()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var brands = await _brandRepository.GetBrands();
            return Ok(brands);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> GetBrand(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var brand = await _brandRepository.GetBrand(id);
            return Ok(brand);
        }
        [HttpPost]
        public async Task<ActionResult> AddBrand(BrandDto brandDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _brandRepository.AddBrand(brandDto);
            return Ok(result);
        }
        [HttpPut]
        public async Task<ActionResult> UpdateBrand(BrandDto brandDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _brandRepository.UpdateBrand(brandDto);
            return Ok(result);
        }
        [HttpDelete]
        public async Task<ActionResult> DeleteBrand(int id)
        {
            var result = _brandRepository.DeleteBrand(id);
            return Ok(result);
        }
    }
}
