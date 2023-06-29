using AutoMapper;
using CarParts.Dto;
using CarParts.SqlServer.DataBase;
using CarParts.Models.Main;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using System.Text.Json;
using CarParts.Models.Security;
using CarParts.SharedKernal.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace CarParts.Data
{
    public class Seeder
    {
        public class JsonBrand
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public string Image { get; set; }
            public string Country { get; set; }
        }
        private readonly CarPartContext _context;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole<int>> _roleManager;
        private readonly IServiceProvider _service;
        private readonly IHostEnvironment _environment;
        public Seeder(CarPartContext context, UserManager<User> userManager, RoleManager<IdentityRole<int>> roleManager, IServiceProvider service, IHostEnvironment environment)
        {
            _context = context;
            _userManager = userManager;
            _service = service;
            _roleManager = roleManager;
            _environment = environment;
        }
        public async Task SeedData()
        {

            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                if (!_context.Brands.Any())
                {
                    string brandsJson = System.IO.File.ReadAllText(@"wwwroot/brands.json");
                    List<JsonBrand> brands = JsonSerializer.Deserialize<List<JsonBrand>>(brandsJson);
                    foreach (var brand in brands)
                    {
                        var countryId = 0;
                        if (_context.Countries.Where(a => a.Name == brand.Country).Any())
                            countryId = _context.Countries.Where(a => a.Name == brand.Country).FirstOrDefault().Id;
                        else
                        {
                            var country = new Country();
                            country.Name = brand.Country;
                            await _context.Countries.AddAsync(country);
                            await _context.SaveChangesAsync();
                            var res = _context.Countries.Where(a => a.Name == brand.Country).FirstOrDefault().Id;
                            countryId = res;
                        }
                        Brand realBrnad = new Brand();
                        realBrnad.Name = brand.Name;
                        realBrnad.CountryId = countryId;
                        realBrnad.Image = brand.Image;
                        await _context.Brands.AddAsync(realBrnad);
                        await _context.SaveChangesAsync();
                    }
                }             
                if (!_context.Roles.Any())
                {
                    var roles = new List<IdentityRole<int>>()
                    {
                        new IdentityRole<int>()
                        {
                            Name = "Admin",
                            ConcurrencyStamp = Guid.NewGuid().ToString()
                        },
                        new IdentityRole<int>()
                        {
                            Name = "CpUser",
                            ConcurrencyStamp = Guid.NewGuid().ToString()
                        },
                        new IdentityRole<int>()
                        {
                            Name = "DataEntry",
                            ConcurrencyStamp = Guid.NewGuid().ToString()
                        }
                    };
                    foreach(var role in roles)
                    {
                        await _roleManager.CreateAsync(role);
                    }
                }
                if (!_context.Users.Any())
                {
                    var user = new User
                    {
                        UserName = "Admin",
                        PhoneNumber = "0911111111",

                    };
                    var result = await _userManager.CreateAsync(user, "admin123");
                    if (result.Succeeded)
                        await _userManager.AddToRoleAsync(user, Role.Admin.ToString());
                    user = new User
                    {
                        UserName = "Sabah",
                        PhoneNumber = "0911111111",
                    };
                    
                    result = await _userManager.CreateAsync(user , "sabah123");
                    if(result.Succeeded)
                        await _userManager.AddToRoleAsync(user, Role.CpUser.ToString());
                }
                if (!_context.Clients.Any())
                {
                    var clients = new List<Client>()
                    {
                        new Client()
                        {
                            Name = "زبون مفرق",
                            PhoneNumber = "0911111111",
                            Address = "",
                            Email = "CarParts@CarParts.com",
                            IsSeller = false,
                            CreatedAt = DateTime.Now,
                        }
                    };
                    await _context.Clients.AddRangeAsync(clients);
                    await _context.SaveChangesAsync();
                }
                await transaction.CommitAsync();
            }
        }
    }
}
