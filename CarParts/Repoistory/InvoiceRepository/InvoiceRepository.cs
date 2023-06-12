using AutoMapper;
using CarParts.Data;
using CarParts.Dto.InvoiceDto;
using CarParts.Models.Main;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CarParts.Repoistory.InvoiceRepository
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly CarPartContext _context;
        private readonly IMapper _mapper;
        public InvoiceRepository(CarPartContext context , IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<IEnumerable<GetInvoiceDto>> GetInvoices()
        {
            var invoices = await _context.Invoices.OrderBy(i => i.Id).ToListAsync();
            var invoicesDto = _mapper.Map<List<GetInvoiceDto>>(invoices);
            return invoicesDto;
        }

        public async Task<GetInvoiceDto> GetInvoice(int id)
        {
            var invoice = await _context.Invoices.FirstOrDefaultAsync(i => i.Id == id);
            var invoiceDto = _mapper.Map<GetInvoiceDto>(invoice);
            return invoiceDto;
        }

        public async Task<IEnumerable<GetInvoiceDto>> GetInvoiceByClient(int clientId)
        {
            var invoices = await _context.Invoices.Where(i => i.ClientId  == clientId).ToListAsync();
            var invoicesDto = _mapper.Map<List<GetInvoiceDto>>(invoices);
            return invoicesDto;
        }

        public async Task<GetInvoiceDto> AddInvoice(AddInvoiceDto invoiceDto)
        {
            var invoice = _mapper.Map<Invoice>(invoiceDto);
            invoice.CreatedAt = DateTime.Now;
            var parts = invoiceDto.Parts;
            Move move = new Move();

            var result = await _context.Invoices.AddAsync(invoice);
            await _context.SaveChangesAsync();
            if(invoiceDto.Parts != null)
                foreach (var part in invoiceDto.Parts)
                {
                    move.InvoiceId = result.Entity.Id;
                    move.Price = part.Price;
                    move.CreatedAt = DateTime.Now;
                    move.Quantity = part.Quantity;
                    var storePart = _context.StoreParts.Where(s => s.PartId == part.PartId && s.StoreId == part.StoreId).FirstOrDefaultAsync().Result;
                    if (invoice.IsImport)
                        storePart.Quantity -= part.Quantity;
                    else
                        storePart.Quantity += part.Quantity;
                    move.StorePartId = storePart.Id;
                    await _context.Moves.AddAsync(move);
                    await _context.SaveChangesAsync();
                    _context.StoreParts.Update(storePart);
                }
            await _context.SaveChangesAsync();
            var getInvoice = _mapper.Map<GetInvoiceDto>(result.Entity);
            return getInvoice;
        }

        public async Task<GetInvoiceDto> UpdateInvoice(UpdateInvoiceDto invoiceDto)
        {
            var invoice = _mapper.Map<Invoice>(invoiceDto);
            invoice.UpdatedAt = DateTime.Now;
            var result = _context.Update(invoice);
            await _context.SaveChangesAsync();
            var getInvoice = _mapper.Map<GetInvoiceDto>(result.Entity);
            return getInvoice;
        }
        public bool DeleteInvoice(int id)
        {
            var invoice = _context.Invoices.FirstOrDefault(i => i.Id == id);
            _context.Remove(invoice);
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
        public async Task<GetAccountDto> GetAccountClient(int clientId)
        {
            var importInovicesAccount = await _context.Invoices.Where(i => i.ClientId == clientId && i.IsImport)
                .Select(i => new {
                    Cost = i.Cost,
                    Services = i.Services
                }).ToListAsync();
            double importCost = importInovicesAccount.Select(i => i.Cost).Sum() + importInovicesAccount.Select(i => i.Services).Sum();
            var exportInvoicesAccount = await _context.Invoices.Where(i => i.ClientId == clientId && !i.IsImport)
                .Select(i => new
                {
                    Cost = i.Cost,
                    Services = i.Services
                }).ToListAsync();
            double exportCost = exportInvoicesAccount.Select(i => i.Cost).Sum() + exportInvoicesAccount.Select(i => i.Services).Sum();
            var account = new GetAccountDto
            {
                ClientId = clientId,
                ExportCost = exportCost,
                ImportCost = importCost
            };
            return account;
        }
    }
}
