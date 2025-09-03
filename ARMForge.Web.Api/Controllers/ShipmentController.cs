using ARMForge.Business.Interfaces;
using ARMForge.Business.Services;
using ARMForge.Kernel.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ARMForge.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShipmentController : ControllerBase
    {
        private readonly IShipmentService _shipmentService;

        public ShipmentController(IShipmentService shipmentService)
        {
            _shipmentService = shipmentService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Shipment>>> GetShipments()
        {
            var shipments = await _shipmentService.GetAllShipmentsAsync();
            return Ok(shipments);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Shipment>> GetShipment(int id)
        {
            var shipment = await _shipmentService.GetShipmentByIdAsync(id);
            if (shipment == null) return NotFound();
            return Ok(shipment);
        }

        [HttpPost]
        public async Task<ActionResult<Shipment>> AddShipment([FromBody] Shipment shipment)
        {
            var addedShipment = await _shipmentService.AddShipmentAsync(shipment);
            return CreatedAtAction(nameof(GetShipment), new { id = addedShipment.Id }, addedShipment);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateShipment(int id, [FromBody] Shipment shipment)
        {
            if (id != shipment.Id) return BadRequest();

            var updatedShipment = await _shipmentService.UpdateShipmentAsync(shipment);
            if (updatedShipment == null) return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShipment(int id)
        {
            var result = await _shipmentService.DeleteShipmentAsync(id);
            if (!result) return NotFound();

            return NoContent();
        }
    }
}
