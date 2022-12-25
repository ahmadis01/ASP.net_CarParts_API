using AutoMapper;
using CarParts.Data;
using CarParts.Dto.CountryDto;
using CarParts.Models.Main;
using Microsoft.EntityFrameworkCore;

namespace CarParts.Repoistory.CountryRepository
{
    public class CountryRepository : ICountryRepository
    {
        private readonly CarPartContext _context;
        private readonly IMapper _mapper;

        public CountryRepository(CarPartContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Country> AddCountry(GetCountryDto countryDto)
        {
            Country country = _mapper.Map<Country>(countryDto);
            country.CreatedAt = DateTime.Now;
            var result = await _context.AddAsync(country);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public bool DeleteCountry(int id)
        {
            var country = _context.Countries.FirstOrDefault(c => c.Id == id);
            _context.Remove(country);
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public async Task<IEnumerable<Country>> GetCountries()
        {
            var countries = await _context.Countries.OrderBy(c => c.Id).ToListAsync();
            return countries;
        }

        public async Task<Country> GetCountry(int id)
        {
            var country = await _context.Countries.FirstOrDefaultAsync(c => c.Id == id);
            return country;
        }

        public async Task<Country> UpdateCountry(GetCountryDto countryDto)
        {
            Country country = _mapper.Map<Country>(countryDto);
            country.UpdatedAt = DateTime.Now;
            var result = _context.Countries.Update(country);
            await _context.SaveChangesAsync();
            return result.Entity;
        }
    }
}
