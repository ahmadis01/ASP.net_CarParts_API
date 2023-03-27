using CarParts.Dto.PartDto;
using CarParts.Parameters;

namespace CarParts.Repoistory.PartRepository
{
    public interface IPartRepository
    {
        Task<GetPartDto> GetParts(PartParameters parameters);
        Task<GetPartData> GetPart(int id);
        Task<GetPartData> AddPart(AddPartDto partDto);
        Task<GetPartData> UpdatePart(UpdatePartDto partDto);
        bool DeletePart(int id);

    }
}
