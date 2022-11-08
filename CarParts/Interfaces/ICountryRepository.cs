using CarParts.Dto;
using CarParts.Models.Main;

namespace CarParts.Interfaces
{
    public interface ICountryRepository
    {
        Task<IEnumerable<Country>> GetCountries();
        Task<Country> GetCountry(int id);
        Task<Country> AddCountry(CountryDto countryDto);
        Task<Country> UpdateCountry(CountryDto countryDto);
        bool DeleteCountry(int id);
    }
}
