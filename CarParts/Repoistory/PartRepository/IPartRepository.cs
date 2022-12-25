using CarParts.Dto.PartDto;

namespace CarParts.Repoistory.PartRepository
{
    public interface IPartRepository
    {
        Task<IEnumerable<GetPartDto>> GetParts();
        Task<GetPartDto> GetPart(int id);
        Task<GetPartDto> GetPart(string name);
        Task<GetPartDto> AddPart(AddPartDto partDto);
        Task<GetPartDto> UpdatePart(UpdatePartDto partDto);
        bool DeletePart(int id);

    }
}
