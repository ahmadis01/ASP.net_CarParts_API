using CarParts.Dto;
using CarParts.Models.Main;

namespace CarParts.Interfaces
{
    public interface ICountryRepository
    {
        Task<IEnumerable<Country>> GetCountries();
        Task<Country> GetCountry(int id);
        Task<Country> AddCountry(Country country);
        Task<Country> UpdateCountry(Country country);
        bool DeleteCountry(int id);
    }
}
