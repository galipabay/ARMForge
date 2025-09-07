using ARMForge.Business.Interfaces;
using ARMForge.Kernel.Entities;
using ARMForge.Types.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ARMForge.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private readonly ISupplierService _supplierService;

        public SupplierController(ISupplierService supplierService)
        {
            _supplierService = supplierService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Supplier>>> GetSuppliers()
        {
            var suppliers = await _supplierService.GetAllSuppliersAsync();
            return Ok(suppliers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Supplier>> GetSupplier(int id)
        {
            var supplier = await _supplierService.GetSupplierByIdAsync(id);
            if (supplier == null) return NotFound();
            return Ok(supplier);
        }

        [HttpPost]
        public async Task<ActionResult<Supplier>> AddSupplier([FromBody] SupplierDto supplierDto)
        {
            // DTO'dan Entity'ye dönüşüm
            var supplier = new Supplier
            {
                CompanyName = supplierDto.CompanyName,
                ContactPerson = supplierDto.ContactPerson,
                Email = supplierDto.Email,
                PhoneNumber = supplierDto.PhoneNumber,
                Address = supplierDto.Address,
                TaxId = supplierDto.TaxId
            };

            var addedSupplier = await _supplierService.AddSupplierAsync(supplier);
            return CreatedAtAction(nameof(GetSupplier), new { id = addedSupplier.Id }, addedSupplier);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSupplier(int id, [FromBody] SupplierDto supplierDto)
        {
            if (id != supplierDto.Id) return BadRequest();

            var existingSupplier = await _supplierService.GetSupplierByIdAsync(id);
            if (existingSupplier == null) return NotFound();

            // DTO'dan mevcut entity'yi güncelleme
            existingSupplier.CompanyName = supplierDto.CompanyName;
            existingSupplier.ContactPerson = supplierDto.ContactPerson;
            existingSupplier.Email = supplierDto.Email;
            existingSupplier.PhoneNumber = supplierDto.PhoneNumber;
            existingSupplier.Address = supplierDto.Address;
            existingSupplier.TaxId = supplierDto.TaxId;

            var updatedSupplier = await _supplierService.UpdateSupplierAsync(existingSupplier);
            if (updatedSupplier == null) return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSupplier(int id)
        {
            var result = await _supplierService.DeleteSupplierAsync(id);
            if (!result) return NotFound();

            return NoContent();
        }
    }
}
