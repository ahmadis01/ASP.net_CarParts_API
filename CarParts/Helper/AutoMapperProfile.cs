using AutoMapper;
using CarParts.Dto.BrandDto;
using CarParts.Dto.CarDto;
using CarParts.Dto.CategoryDto;
using CarParts.Dto.CountryDto;
using CarParts.Dto.PartDto;
using CarParts.Models.Main;

namespace CarParts.Helper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //Car
            CreateMap<Car, GetCarDto>();
            CreateMap<AddCarDto, Car>();
            CreateMap<UpdateCarDto, Car>();
            //Branad
            CreateMap<Brand, GetBrandDto>();
            CreateMap<AddBrandDto, Brand>();
            CreateMap<UpdateBrandDto, Brand>();
            //Country
            CreateMap<Country, GetCountryDto>();
            CreateMap<AddCountryDto, Country>();
            //Category
            CreateMap<Category, GetCategoryDto>();
            CreateMap<AddCategoryDto,Category>();
            CreateMap<UpdateCategoryDto, Category>();
            //Part
            CreateMap<Part, GetPartDto >();
            CreateMap<AddPartDto, Part>();
            CreateMap<UpdatePartDto, Part>();
        }
    }
}
