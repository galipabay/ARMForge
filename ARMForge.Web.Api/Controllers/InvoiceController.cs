using ARMForge.Business.Interfaces;
using ARMForge.Kernel.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ARMForge.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceService _invoiceService;

        public InvoiceController(IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }

        // Tüm faturaları getiren endpoint
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Invoice>>> Get()
        {
            var invoices = await _invoiceService.GetAllInvoicesAsync();
            return Ok(invoices);
        }

        // ID'ye göre tek bir fatura getiren endpoint
        [HttpGet("{id}")]
        public async Task<ActionResult<Invoice>> Get(int id)
        {
            var invoice = await _invoiceService.GetInvoiceByIdAsync(id);
            if (invoice == null)
            {
                return NotFound();
            }
            return Ok(invoice);
        }

        // Yeni bir fatura ekleyen endpoint
        [HttpPost]
        public async Task<ActionResult<Invoice>> Post([FromBody] Invoice invoice)
        {
            var addedInvoice = await _invoiceService.AddInvoiceAsync(invoice);
            return CreatedAtAction(nameof(Get), new { id = addedInvoice.Id }, addedInvoice);
        }

        // Bir faturayı güncelleyen endpoint
        [HttpPut("{id}")]
        public async Task<ActionResult<Invoice>> Put(int id, [FromBody] Invoice invoice)
        {
            if (id != invoice.Id)
            {
                return BadRequest();
            }

            await _invoiceService.UpdateInvoiceAsync(invoice);
            return Ok(invoice);
        }

        // Bir faturayı silen endpoint
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _invoiceService.DeleteInvoiceAsync(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
