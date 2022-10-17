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
            CreateMap<Brand, BrandDto>();
        }
    }
}
