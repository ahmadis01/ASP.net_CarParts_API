using CarParts.Dto.InvoiceDto;
using CarParts.Repoistory.InvoiceRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarParts.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceRepository _invoiceRepository;
        public InvoiceController(IInvoiceRepository invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetInvoices()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var invoices = await _invoiceRepository.GetInvoices();
            return Ok(invoices);   
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetInvoice(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var invoice = await _invoiceRepository.GetInvoice(id);
            return Ok(invoice);
        }
        [HttpPost]
        public async Task<IActionResult> AddInvoice([FromBody] AddInvoiceDto invoiceDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var invoice = await _invoiceRepository.AddInvoice(invoiceDto);
            return Ok(invoice);

        }
        [HttpGet("GetAccountClient/{clientId}")]
        public async Task<IActionResult> GetAccountClient(int clientId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var account = await _invoiceRepository.GetAccountClient(clientId);
            return Ok(account); 
        }

    }
}
