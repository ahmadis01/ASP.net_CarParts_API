using AutoMapper;
using CarParts.SqlServer.DataBase;
using CarParts.Dto.InvoiceDto;
using CarParts.Models.Main;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using CarParts.SharedKernal.Enums;

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
            var partsIds = invoiceDto.Parts.Select(p => p.PartId);
            var storeParts = _context.StoreParts.Where(s => partsIds.Contains(s.PartId));
            var result = await _context.Invoices.AddAsync(invoice);
            await _context.SaveChangesAsync();
            if(invoiceDto.InvoiceType == InvoiceType.PurchaseInvoice || invoiceDto.InvoiceType == InvoiceType.SellInvoice)
                foreach (var part in invoiceDto.Parts)
                {
                    move.InvoiceId = result.Entity.Id;
                    move.Price = part.Price;
                    move.CreatedAt = DateTime.Now;
                    move.Quantity = part.Quantity;
                    var storePart = storeParts.Where(s => s.PartId == part.PartId && s.StoreId == part.StoreId).FirstOrDefault();
                    storePart.Quantity = invoiceDto.InvoiceType == InvoiceType.PurchaseInvoice ? storePart.Quantity += part.Quantity : storePart.Quantity -= part.Quantity;
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
        public async Task<List<GetAccountDto>> GetAccountClient(int clientId)
        {
            List<GetAccountDto> accountDto = new List<GetAccountDto>();
            var invoices = _context.Invoices.Where(i => i.ClientId == clientId).OrderByDescending(i => i.CreatedAt);
            foreach (var invoice in invoices)
            {
                var account = new GetAccountDto
                {
                    Cost = invoice.Cost,
                    Description = invoice.Description,
                    InvoiceType = invoice.InvoiceType,
                    Services = invoice.Services
                };
                accountDto.Add(account);
            }
            return accountDto;
        }
    }
}
