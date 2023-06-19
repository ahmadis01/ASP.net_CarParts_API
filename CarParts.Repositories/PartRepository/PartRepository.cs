﻿using AutoMapper;
using CarParts.SqlServer.DataBase;
using CarParts.Dto.CarPartsDto;
using CarParts.Dto.PartDto;
using CarParts.Models.Main;
using Microsoft.EntityFrameworkCore;
using System.IO;
using CarParts.SharedKernal.Parameters;
using Microsoft.Extensions.Hosting;

namespace CarParts.Repoistory.PartRepository
{
    public class PartRepository : IPartRepository
    {
        private readonly CarPartContext _context;
        private readonly IMapper _mapper;
        private readonly IHostEnvironment _environment;


        public PartRepository(CarPartContext context,IMapper mapper, IHostEnvironment environment)
        {
            _context = context;
            _mapper = mapper;
            _environment = environment;
        }
        public async Task<GetPartDto> GetParts(PartParameters parameters)
        {
            var partsData = new GetPartDto();
            List<Part> parts = await _context.Parts.OrderByDescending(p => p.CreatedAt)
                    .Include(p => p.Brand)
                    .Include(p => p.StoreParts)
                    .Include(p => p.CarParts).ToListAsync();
            parts = Filter(parameters, parts);
            partsData.TotalNumber = parts.Count;
            parts = Pagination(parameters ,parts);


            if (!String.IsNullOrEmpty(parameters.Search.Trim()))
            {
                parts = parts.Where(p => p.Name.Contains(parameters.Search)).ToList();
            }

            var partsDto = _mapper.Map<List<GetPartData>>(parts);

            foreach (var partDto in partsDto)
                partDto.Cars = _context.CarParts.Where(c => c.PartId == partDto.Id).Select(c => c.CarId).ToList();

            partsData.Parts = partsDto;
            return partsData;
        }
        public async Task<GetPartData> AddPart(AddPartDto partDto)
        {
            var part = _mapper.Map<Part>(partDto);            
            part.CreatedAt = DateTime.Now;
            //upload image
            var fileName = DateTime.Now.Ticks.ToString();
            var extension = Path.GetExtension(partDto.Image.FileName);
            var path = Path.Combine("wwwroot/Images/Parts", fileName + extension);
            part.Image = Path.Combine("/Images/Parts", fileName + extension);
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                await partDto.Image.CopyToAsync(stream);
                stream.Close();
            }
            var partRes = await _context.Parts.AddAsync(part);
            await _context.SaveChangesAsync();
            //connect the part with cars
            for (int i = 0; i < partDto.CarIds.Count; i++)
            {
                var carPart = new CarPart();
                carPart.CarId = partDto.CarIds[i];
                carPart.PartId = partRes.Entity.Id;
                await _context.CarParts.AddAsync(carPart);
                await _context.SaveChangesAsync();
            }
            //connect the part with store
            var storePart = new StorePart();
            storePart.PartId = partRes.Entity.Id;
            storePart.StoreId = partDto.StoreId;
            storePart.Quantity = partDto.Quantity;

            await _context.StoreParts.AddAsync(storePart);
            await _context.SaveChangesAsync();
            var getPart = _mapper.Map<GetPartData>(part);
            return getPart;
        }
        public bool DeletePart(int id)
        {
            var part = _context.Parts.FirstOrDefault(p => p.Id == id);
            var oldPartPath = _environment.ContentRootPath.ToString() + "wwwroot" + part.Image;
            if (File.Exists(oldPartPath))
                File.Delete(oldPartPath);
            _context.Parts.Remove(part);
            var result = _context.SaveChanges();
            return result > 0 ? true : false;
        }
        public async Task<GetPartData> GetPart(int id)
        {
            var part = await _context.Parts.Include(p => p.CarParts)
                .Include(p => p.StoreParts)
                .FirstOrDefaultAsync(c => c.Id == id);
            var partDto = _mapper.Map<GetPartData>(part);
            return partDto;       
        }
        public async Task<GetPartData> UpdatePart(UpdatePartDto partDto)
        {
            var oldPart = _context.Parts.AsNoTracking().FirstOrDefaultAsync(b => b.Id == partDto.Id).Result;
            var oldPartDto = _environment.ContentRootPath.ToString() + "wwwroot" + oldPart.Image;
            if (File.Exists(oldPartDto))
                File.Delete(oldPartDto);
            var part = _mapper.Map<Part>(partDto);
            var fileName = DateTime.Now.Ticks.ToString();
            var extension = Path.GetExtension(partDto.Image.FileName);
            var path = Path.Combine("wwwroot/Images/Parts", fileName + extension);
            part.Image = Path.Combine("/Images/Parts", fileName + extension);
            part.UpdatedAt = DateTime.Now;
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                await partDto.Image.CopyToAsync(stream);
                stream.Close();
            }
            var result = _context.Parts.Update(part);
            await _context.SaveChangesAsync();
            var getPart = _mapper.Map<GetPartData>(result.Entity);
            return getPart;
        }
        public List<Part> Filter(PartParameters parameters , List<Part> parts)
        {

            if (parameters.CountryId != 0)
            {
                parts = parts.Where(p => p.Brand.CountryId == parameters.CountryId).ToList();
            }
            if (parameters.BrandId != 0)
            {
                parts = parts.Where(p => p.BrandId == parameters.BrandId).ToList();
            }
            if (parameters.StoreId != 0)
            {
                var stores = _context.StoreParts.Where(s => s.StoreId == parameters.StoreId).Select(s => s.PartId).ToList();
                parts = parts.Join(stores, p => p.Id, id => id, (p, id) => p).ToList();
            }
            if(parameters.CarId != 0)
            {
                var cars = _context.CarParts.Where(s => s.CarId == parameters.CarId).Select(c => c.PartId).ToList();
                parts = parts.Join(cars, p => p.Id, id => id, (p, id) => p).ToList();
            }
            if (!String.IsNullOrEmpty(parameters.OrderBy))
            {
                switch (parameters.OrderBy)
                {
                    case "name":
                        parts = parts.OrderBy(p => p.Name).ToList();
                        break;
                    case "price":
                        parts = parts.OrderBy(p => p.SellingPrice).ToList();
                        break;
                    case "date":
                        parts = parts.OrderBy(p => p.CreatedAt).ToList();
                        break;
                }
            }
            if(parameters.OrderStatus.ToLower() == "desc")
            {
                parts.Reverse();
            }
            return parts;
        }
        public List<Part> Pagination(PartParameters parameters,List<Part> parts)
        {
            if (parameters.PageSize != 0 && parameters.PageNumber != 0)
            {
                parts = parts
                    .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                    .Take(parameters.PageSize)
                    .ToList();
            }
            return parts;
        }
    }
}