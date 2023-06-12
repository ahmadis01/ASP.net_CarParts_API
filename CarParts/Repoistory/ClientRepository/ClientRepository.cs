using AutoMapper;
using CarParts.Data;
using CarParts.Dto.ClientDto;
using CarParts.Dto.InvoiceDto;
using CarParts.Models.Main;
using Microsoft.EntityFrameworkCore;

namespace CarParts.Repoistory.ClientRepository
{
    public class ClientRepository : IClientRepository
    {
        private readonly CarPartContext _context;
        private readonly IMapper _mapper;
        public ClientRepository(CarPartContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<IEnumerable<GetClientsDto>> GetClients()
        {
            var clients = await _context.Clients.OrderBy(c => c.Id).ToListAsync();
            var clientsDto = _mapper.Map<List<GetClientsDto>>(clients);
            foreach (var client in clientsDto)
            {
                client.TotalAccount = await GetClientAccount(client.Id);
            }
            return clientsDto;
        }

        public async Task<GetClientDto> GetClient(int id)
        {
            var client = await _context.Clients.FirstOrDefaultAsync(c => c.Id == id);
            var clientDto =  _mapper.Map<GetClientDto>(client);
            return clientDto;
        }

        public async Task<IEnumerable<GetClientDto>> GetClient(string name)
        {
            var clients = await _context.Clients.Where(c => c.Name == name).ToListAsync();
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

        public async Task<int> GetClientAccount(int clientId)
        {
            var client = await _context.Clients.FirstOrDefaultAsync(c => c.Id == clientId);
            var importInovicesAccount = await _context.Invoices.Where(i => i.ClientId == clientId && i.IsImport)
                .Select(i => new {
                    Cost = i.Cost,
                    Services = i.Services
                }).ToListAsync();
            int importCost = importInovicesAccount.Select(i => i.Cost).Sum() + importInovicesAccount.Select(i => i.Services).Sum();
            var exportInvoicesAccount = await _context.Invoices.Where(i => i.ClientId == clientId && !i.IsImport)
                .Select(i => new
                {
                    Cost = i.Cost,
                    Services = i.Services
                }).ToListAsync();
            int exportCost = exportInvoicesAccount.Select(i => i.Cost).Sum() + exportInvoicesAccount.Select(i => i.Services).Sum();
            var account = importCost - exportCost;
            return account;
        }

    }
}
