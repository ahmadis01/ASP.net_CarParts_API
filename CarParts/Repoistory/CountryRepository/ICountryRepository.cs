using CarParts.Dto.CountryDto;
using CarParts.Models.Main;

namespace CarParts.Repoistory.CountryRepository
{
    public interface ICountryRepository
    {
        Task<IEnumerable<Country>> GetCountries();
        Task<Country> GetCountry(int id);
        Task<Country> AddCountry(GetCountryDto countryDto);
        Task<Country> UpdateCountry(GetCountryDto countryDto);
        bool DeleteCountry(int id);
    }
}
