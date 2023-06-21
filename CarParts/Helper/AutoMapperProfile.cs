using AutoMapper;
using CarParts.Dto.BrandDto;
using CarParts.Dto.CarCategoryDto;
using CarParts.Dto.CarDto;
using CarParts.Dto.CarPartsDto;
using CarParts.Dto.CategoryDto;
using CarParts.Dto.ClientDto;
using CarParts.Dto.CountryDto;
using CarParts.Dto.InvoiceDto;
using CarParts.Dto.PartDto;
using CarParts.Dto.StoreDto;
using CarParts.Dto.StorePartDto;
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
            CreateMap<Part, GetPartData >();
            CreateMap<AddPartDto, Part>();
            CreateMap<UpdatePartDto, Part>();
            //CarPart
            CreateMap<CarPart, GetCarPartDto>();
            CreateMap<AddCarPartDto, CarPart>();
            CreateMap<UpdateCarPartDto , CarPart>();
            //Store
            CreateMap<Store, GetStoreDto>();
            CreateMap<AddStoreDto, Store>();
            CreateMap<UpdateStoreDto, Store>();
            //StoreCP
            CreateMap<StorePart, GetStorePartDto>();
            //Client
            CreateMap<Client, GetClientsDto>();
            CreateMap<Client, GetClientDto>();
            CreateMap<AddClientDto, Client>();
            CreateMap<UpdateClientDto,Client>();
            //Invoice
            CreateMap<Invoice, GetInvoiceDto>();
            CreateMap<UpdateInvoiceDto, Invoice>();
            CreateMap<AddInvoiceDto, Invoice>();
            //CarCategory
            CreateMap<CarCategory, GetCarCategoryDto>();
            CreateMap<AddCarCategoryDto, CarCategory>();
        }
    }
}
