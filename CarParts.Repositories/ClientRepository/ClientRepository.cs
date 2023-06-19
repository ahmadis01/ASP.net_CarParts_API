using AutoMapper;
using CarParts.SqlServer.DataBase;
using CarParts.Dto.ClientDto;
using CarParts.Dto.InvoiceDto;
using CarParts.Models.Main;
using Microsoft.EntityFrameworkCore;
using CarParts.SharedKernal.Enums;

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
            var totalAccount = 0;
            var invoices = _context.Invoices.Where(i => i.ClientId == clientId).OrderByDescending(i => i.CreatedAt);
            foreach (var invoice in invoices)
            {
                if (invoice.InvoiceType == InvoiceType.PurchaseInvoice || invoice.InvoiceType == InvoiceType.OutgoingPayment)
                {
                    totalAccount += invoice.Cost + invoice.Services;
                }
                else if (invoice.InvoiceType == InvoiceType.SellInvoice || invoice.InvoiceType == InvoiceType.IncomingPayment)
                    totalAccount -= invoice.Cost + invoice.Services;
            }
            return totalAccount;
            
        }
    }
}
