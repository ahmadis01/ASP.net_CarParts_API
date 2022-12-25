using AutoMapper;
using CarParts.Data;
using CarParts.Dto.PartDto;
using CarParts.Models.Main;
using Microsoft.EntityFrameworkCore;

namespace CarParts.Repoistory.PartRepository
{
    public class PartRepository : IPartRepository
    {
        private readonly CarPartContext _context;
        private readonly IMapper _mapper;

        public PartRepository(CarPartContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<GetPartDto>> GetParts()
        {
            var parts = await _context.Parts.OrderBy(p => p.Id).ToListAsync();
            var partsDto = _mapper.Map<List<GetPartDto>>(parts);
            return partsDto;
        }
        public async Task<GetPartDto> AddPart(AddPartDto partDto)
        {
            var part = _mapper.Map<Part>(partDto);            
            part.CreatedAt = DateTime.Now;
            var partRes = await _context.Parts.AddAsync(part);
            await _context.SaveChangesAsync();
            var getPart = _mapper.Map<GetPartDto>(part);
            return getPart;
        }

        public bool DeletePart(int id)
        {
            var part = _context.Parts.FirstOrDefault(p => p.Id == id);
            _context.Parts.Remove(part);
            var result = _context.SaveChanges();
            return result > 0 ? true : false;
        }

        public async Task<GetPartDto> GetPart(int id)
        {
            var part = await _context.Parts.FirstOrDefaultAsync(c => c.Id == id);
            var partDto = _mapper.Map<GetPartDto>(part);
            return partDto;       
        }

        public async Task<GetPartDto> GetPart(string name)
        {
            var part = await _context.Parts.FirstOrDefaultAsync(p => p.Name == name);
            var partDto = _mapper.Map<GetPartDto>(part);
            return partDto;
        }

        public async Task<GetPartDto> UpdatePart(UpdatePartDto partDto)
        {
            var part = _mapper.Map<Part>(partDto);
            var result = _context.Parts.Update(part);
            await _context.SaveChangesAsync();
            var getPart = _mapper.Map<GetPartDto>(result.Entity);
            return getPart;

        }
    }
}
