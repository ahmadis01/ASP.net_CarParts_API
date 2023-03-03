using AutoMapper;
using CarParts.Data;
using CarParts.Dto.InvoiceDto;
using CarParts.Models.Main;
using Microsoft.EntityFrameworkCore;

namespace CarParts.Repoistory.InvoiceRepository
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly CarPartContext context;
        private readonly IMapper mapper;

        public InvoiceRepository(CarPartContext context , IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public async Task<IEnumerable<GetInvoiceDto>> GetInvoices()
        {
            var invoices = await context.Invoices.OrderBy(i => i.Id).ToListAsync();
            var invoicesDto = mapper.Map<List<GetInvoiceDto>>(invoices);
            return invoicesDto;
        }

        public async Task<GetInvoiceDto> GetInvoice(int id)
        {
            var invoice = await context.Invoices.FirstOrDefaultAsync(i => i.Id == id);
            var invoiceDto = mapper.Map<GetInvoiceDto>(invoice);
            return invoiceDto;
        }

        public async Task<IEnumerable<GetInvoiceDto>> GetInvoiceByClient(int clientId)
        {
            var invoices = await context.Invoices.Where(i=>i.ClientId  == clientId).ToListAsync();
            var invoicesDto = mapper.Map<List<GetInvoiceDto>>(invoices);
            return invoicesDto;
        }

        public async Task<GetInvoiceDto> AddInvoice(AddInvoiceDto invoiceDto)
        {
            var invoice = mapper.Map<Invoice>(invoiceDto);
            invoice.CreatedAt = DateTime.Now;
            var result = await context.Invoices.AddAsync(invoice);
            await context.SaveChangesAsync();
            var getInvoice = mapper.Map<GetInvoiceDto>(result.Entity);
            return getInvoice;
        }

        public async Task<GetInvoiceDto> UpdateInvoice(UpdateInvoiceDto invoiceDto)
        {
            var invoice = mapper.Map<Invoice>(invoiceDto);
            invoice.UpdatedAt = DateTime.Now;
            var result = context.Update(invoice);
            await context.SaveChangesAsync();
            var getInvoice = mapper.Map<GetInvoiceDto>(result.Entity);
            return getInvoice;
        }
        public bool DeleteInvoice(int id)
        {
            var invoice = context.Invoices.FirstOrDefault(i => i.Id == id);
            context.Remove(invoice);
            var saved = context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
