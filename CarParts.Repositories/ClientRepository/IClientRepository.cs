using CarParts.Dto.ClientDto;

namespace CarParts.Repoistory.ClientRepository
{
    public interface IClientRepository
    {
        Task<IEnumerable<GetClientsDto>> GetClients();
        Task<GetClientDto> GetClient(int id);
        Task<IEnumerable<GetClientDto>> GetClient(string name);
        Task<GetClientDto> AddClient(AddClientDto clientDto);
        Task<GetClientDto> UpdateClient(UpdateClientDto clientDto);
        bool DeleteClient(int id);
    }
}
