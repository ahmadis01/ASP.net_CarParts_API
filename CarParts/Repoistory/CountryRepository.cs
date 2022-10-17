using CarParts.Data;
using CarParts.Interfaces;
using CarParts.Models.Main;
using Microsoft.EntityFrameworkCore;

namespace CarParts.Repoistory
{
    public class CountryRepository : ICountryRepository
    {
        private readonly CarPartContext _context;

        public CountryRepository(CarPartContext context)
        {
            _context = context;
        }
        public async Task<Country> AddCountry(Country country)
        {
            var result = await _context.AddAsync(country);
            return result.Entity;
        }

        public bool DeleteCountry(int id)
        {
            var country = _context.Countries.FirstOrDefault(c => c.Id == id); 
            _context.Remove(country);
            var saved =  _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public async Task<IEnumerable<Country>> GetCountries()
        {
            var countries =await _context.Countries.OrderBy(c => c.Id).ToListAsync();
            return countries;
        }

        public async Task<Country> GetCountry(int id)
        {
            var country = await _context.Countries.FirstOrDefaultAsync(c => c.Id == id);
            return country;
        }

        public async Task<Country> UpdateCountry(Country country)
        {
            var result = _context.Countries.Update(country);
            await _context.SaveChangesAsync();
            return result.Entity;
        }
    }
}
