using CarParts.Interfaces;
using CarParts.Models.Main;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarParts.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly ICountryRepository _countryRepository;

        public CountryController(ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }
        [HttpGet]
        public async Task<ActionResult> GetCounries()
        {
            var countries =await _countryRepository.GetCountries();
            return Ok(countries);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> GetCountyry(int id)
        {
            var country = await _countryRepository.GetCountry(id);
            return Ok(country);
        }
        [HttpPost]
        public async Task<ActionResult> AddCountry(Country country)
        {
            var result =await _countryRepository.AddCountry(country);
            return Ok(result);
        }
        [HttpPut]
        public async Task<ActionResult> UpdateCountry(Country country)
        {
            var result = await _countryRepository.UpdateCountry(country);
            return Ok(result);
        }
    }
}
