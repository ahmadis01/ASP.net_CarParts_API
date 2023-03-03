using CarParts.Dto.InvoiceDto;

namespace CarParts.Repoistory.InvoiceRepository
{
    public interface IInvoiceRepository
    {
        Task<IEnumerable<GetInvoiceDto>> GetInvoices();
        Task<GetInvoiceDto> GetInvoice(int id);
        Task<IEnumerable<GetInvoiceDto>> GetInvoiceByClient(int clientId);
        Task<GetInvoiceDto> AddInvoice(AddInvoiceDto invoiceDto);
        Task<GetInvoiceDto> UpdateInvoice(UpdateInvoiceDto invoiceDto);
        bool DeleteInvoice(int id);
    }
}