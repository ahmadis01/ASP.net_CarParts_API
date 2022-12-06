using AutoMapper;
using CarParts.Dto;
using CarParts.Models.Main;

namespace CarParts.Helper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Car, CarDto>();
            CreateMap<CarDto, Car>();
            CreateMap<Brand, BrandDto>();
            CreateMap<BrandDto, Brand>();
            CreateMap<Country, CountryDto>();
            CreateMap<CountryDto, Country>();
        }
    }
}
