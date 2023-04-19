using AutoMapper;
using CarParts.Dto;
using CarParts.Models.Main;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using System.Text.Json;

namespace CarParts.Data
{
    public class BrandsSeed
    {
        private readonly CarPartContext context;
        private readonly IMapper mapper;
        public class JsonBrand
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public string Image { get; set; }
            public string Country { get; set; }
        }
        public BrandsSeed(CarPartContext context , IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public async Task SeedData()
        {
            if (!context.Brands.Any())
            {
                string brandsJson = System.IO.File.ReadAllText(@"wwwroot/brands.json");
                List<JsonBrand> brands = JsonSerializer.Deserialize<List<JsonBrand>>(brandsJson);
                foreach (var brand in brands)
                {
                    var countryId = 0;
                    if (context.Countries.Where(a => a.Name == brand.Country).Any())
                        countryId = context.Countries.Where(a => a.Name == brand.Country).FirstOrDefault().Id;
                    else
                    {
                        var country = new Country();
                        country.Name = brand.Country;
                        await context.Countries.AddAsync(country);
                        await context.SaveChangesAsync();
                        var res = context.Countries.Where(a => a.Name == brand.Country).FirstOrDefault().Id;
                        countryId = res;
                    }
                    Brand realBrnad = new Brand();
                    realBrnad.Name = brand.Name;
                    realBrnad.CountryId = countryId;
                    realBrnad.Image = brand.Image;
                    await context.Brands.AddAsync(realBrnad);
                    await context.SaveChangesAsync();
                }
            }
            
        }
    }
}
