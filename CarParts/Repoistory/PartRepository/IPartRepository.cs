using CarParts.Dto.PartDto;
using CarParts.Parameters;

namespace CarParts.Repoistory.PartRepository
{
    public interface IPartRepository
    {
        Task<IEnumerable<GetPartDto>> GetParts(PartParameters parameters);
        Task<GetPartDto> GetPart(int id);
        Task<List<GetPartDto>> GetPart(string name);
        Task<GetPartDto> AddPart(AddPartDto partDto);
        Task<GetPartDto> UpdatePart(UpdatePartDto partDto);
        bool DeletePart(int id);

    }
}
