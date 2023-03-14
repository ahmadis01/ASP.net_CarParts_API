using CarParts.Dto.CarPartsDto;
using CarParts.Parameters;
using CarParts.Repoistory.CarPartRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarParts.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarPartController : ControllerBase
    {
        private readonly ICarPartRepository _carPartRepository;

        public CarPartController(ICarPartRepository carPartRepository)
        {
            _carPartRepository = carPartRepository;
        }
        [HttpGet]
        public async Task<ActionResult> GetCarParts([FromQuery]PartParameters carPartParameters)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var carParts = await _carPartRepository.GetCarParts(carPartParameters);
            return Ok(carParts);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> GetCarPart(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var carPart = await _carPartRepository.GetCarPart(id);
            return Ok(carPart);
        }
        [HttpPost]
        public async Task<ActionResult> AddCarPart([FromForm] AddCarPartDto carPartDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _carPartRepository.AddCarPart(carPartDto);
            return Ok(result);
        }
        [HttpPost("/api/CarPart/AddCarPartToNewStore")]
        public async Task<ActionResult> AddCarPartToNewStore([FromForm] AddCarPartToNewStoreDto carPartDto)
        {
            var result = await _carPartRepository.AddCarPartToNewStore(carPartDto);
            return Ok(result);
        }
        [HttpPut]
        public async Task<ActionResult> UpdateCarPart([FromForm] UpdateCarPartDto carPartDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _carPartRepository.UpdateCarPart(carPartDto);
            return Ok(result);
        }
        [HttpDelete]
        public async Task<ActionResult> DeleteCarPart(int id)
        {
            var result = _carPartRepository.DeleteCarPart(id);
            return Ok(result);
        }
    }
}
