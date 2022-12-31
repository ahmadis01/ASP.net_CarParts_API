using CarParts.Dto.StoreDto;

namespace CarParts.Repoistory.StoreRepository
{
    public interface IStoreRepository
    {
        Task<IEnumerable<GetStoreDto>> GetStores();
        Task<GetStoreDto> GetStore(int id);
        Task<GetStoreDto> AddStore(AddStoreDto storeDto);
        Task<GetStoreDto> UpdateStore(UpdateStoreDto storeDto);
        bool DeleteStore(int id);
    }
}
