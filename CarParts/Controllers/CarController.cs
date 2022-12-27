using CarParts.Dto.CarDto;
using CarParts.Models.Main;
using CarParts.Models.Security;
using CarParts.Repoistory.CarRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CarParts.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly ICarRepository carRepository;

        public CarController(ICarRepository carRepository)
        {
            this.carRepository = carRepository;
        }
        [HttpGet]
        public async Task<ActionResult> GetCars()
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var cars = await carRepository.GetCars();
            return Ok(cars);
        }
        [HttpGet("/api/GetCarById/{id}")]
        public async Task<ActionResult> GetCar(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var car = await carRepository.GetCar(id);
            return Ok(car);
        }
        [HttpGet("/api/Car/{name}")]
        public async Task<ActionResult> GetCar(string name)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var car = await carRepository.GetCar(name);
            return Ok(car);
        }
        [HttpPost]
        public async Task<ActionResult> AddCar([FromForm] AddCarDto car)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var result = await carRepository.AddCar(car);
            return Ok(result);
        }
        [HttpPut]
        public async Task<ActionResult> UpdateCar([FromForm] UpdateCarDto car)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var result = await carRepository.UpdateCar(car);
            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCar(int id)
        {
            carRepository.DeleteCar(id);
            return Ok();
        }
    }
}
