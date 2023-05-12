using AutoMapper;
using CarParts.Data;
using CarParts.Dto.StoreDto;
using CarParts.Models.Main;
using Microsoft.EntityFrameworkCore;

namespace CarParts.Repoistory.StoreRepository
{
    public class StoreRepository : IStoreRepository
    {
        private readonly CarPartContext _context;
        private readonly IMapper _mapper;
        public StoreRepository(CarPartContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetStoreDto>> GetStores()
        {
            var stores = await _context.Stores.OrderBy(s => s.Id).ToListAsync();
            var storesDto = _mapper.Map<List<GetStoreDto>>(stores);
            foreach (var store in storesDto)
                store.TotalParts = _context.StoreParts.Where(s => s.StoreId == store.Id).Count();
            return storesDto;
        }

        public async Task<GetStoreDto> GetStore(int id)
        {
            var store =await _context.Stores.FirstOrDefaultAsync(s => s.Id == id);
            var storeDto = _mapper.Map<GetStoreDto>(store);
            storeDto.TotalParts = _context.StoreParts.Where(s => s.StoreId == store.Id).Count();
            return storeDto;
        }

        public async Task<GetStoreDto> AddStore(AddStoreDto storeDto)
        {
            var store = _mapper.Map<Store>(storeDto);
            store.CreatedAt = DateTime.Now;
            var result = await _context.Stores.AddAsync(store);
            await _context.SaveChangesAsync();
            var getStore = _mapper.Map<GetStoreDto>(result.Entity);
            return getStore;
        }

        public async Task<GetStoreDto> UpdateStore(UpdateStoreDto storeDto)
        {
            var store = _mapper.Map<Store>(storeDto);
            store.UpdatedAt = DateTime.Now;
            var result = _context.Stores.Update(store);
            await _context.SaveChangesAsync();
            var getStore = _mapper.Map<GetStoreDto>(result.Entity);
            return getStore;
        }

        public bool DeleteStore(int id)
        {
            var store = _context.Stores.FirstOrDefault(s => s.Id == id);
            _context.Stores.Remove(store);
            var result = _context.SaveChanges();
            return result > 0 ? true : false;
        }
    }
}
