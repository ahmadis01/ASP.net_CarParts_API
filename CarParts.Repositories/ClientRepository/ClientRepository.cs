using AutoMapper;
using CarParts.SqlServer.DataBase;
using CarParts.Dto.ClientDto;
using CarParts.Dto.InvoiceDto;
using CarParts.Models.Main;
using Microsoft.EntityFrameworkCore;
using CarParts.SharedKernal.Enums;
using CarParts.Repositories.SharedRepository;

namespace CarParts.Repoistory.ClientRepository
{
    public class ClientRepository : IClientRepository
    {
        private readonly CarPartContext _context;
        private readonly ISharedRepository _sharedRepository;
        private readonly IMapper _mapper;
        public ClientRepository(CarPartContext context, IMapper mapper, ISharedRepository sharedRepository)
        {
            _context = context;
            _mapper = mapper;
            _sharedRepository = sharedRepository;
        }
        public async Task<IEnumerable<GetClientsDto>> GetClients()
        {
            var clients = await _context.Clients.OrderBy(c => c.Id).Select(c => new 
            {
                Id = c.Id,
                Name = c.Name,
                Address = c.Address,
                Email = c.Email,
                PhoneNumber = c.PhoneNumber,
                IsSeller = c.IsSeller,
                Invoices = c.Invoices,
            }).ToListAsync();
            var clientsDto = new List<GetClientsDto>();
            foreach (var client in clients)
            {
                clientsDto.Add(new GetClientsDto
                {
                    Id = client.Id,
                    Address = client.Address,
                    Name = client.Name,
                    Email = client.Email,
                    PhoneNumber = client.PhoneNumber,
                    IsSeller = client.IsSeller,
                    TotalAccount = await _sharedRepository.CalculateTotalAccount(client.Invoices)
                });
            }
            return clientsDto;
        }

        public async Task<GetClientDto> GetClient(int id)
        {
            var client = await _context.Clients.Include(c => c.Invoices).FirstOrDefaultAsync(c => c.Id == id);
            var clientDto =  _mapper.Map<GetClientDto>(client);
            clientDto.TotalAccount = await _sharedRepository.CalculateTotalAccount(client.Invoices);
            return clientDto;
        }

        public async Task<IEnumerable<GetClientDto>> GetClient(string name)
        {
            var clients = await _context.Clients.Include(c => c.Invoices).Where(c => c.Name == name).ToListAsync();
            var clientsDto = _mapper.Map<List<GetClientDto>>(clients);
            return clientsDto;
        }

        public async Task<GetClientDto> AddClient(AddClientDto clientDto)
        {
            var client = _mapper.Map<Client>(clientDto);
            client.CreatedAt = DateTime.Now;
            var result = await _context.Clients.AddAsync(client);
            await _context.SaveChangesAsync();
            var getClient = _mapper.Map<GetClientDto>(result.Entity);
            return getClient;
        }

        public async Task<GetClientDto> UpdateClient(UpdateClientDto clientDto)
        {
            var client = _mapper.Map<Client>(clientDto);
            client.UpdatedAt = DateTime.Now;
            var result =  _context.Clients.Update(client);
            await _context.SaveChangesAsync();
            var getClient = _mapper.Map<GetClientDto>(result.Entity);
            return getClient;
        }

        public bool DeleteClient(int id)
        {
            var client = _context.Clients.FirstOrDefault(c => c.Id == id);
            _context.Clients.Remove(client);
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
