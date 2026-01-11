using ARMForge.Business.Interfaces;
using ARMForge.Business.Services;
using ARMForge.Kernel.Entities;
using ARMForge.Types.DTOs;
using Humanizer;
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
        public async Task<ActionResult<IEnumerable<ShipmentDto>>> GetAllShipments()
        {
            var shipments = await _shipmentService.GetAllShipmentsAsync();
            return Ok(shipments); // artık service DTO döndürüyor
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ShipmentDto>> GetShipment(int id)
        {
            var shipmentDto = await _shipmentService.GetShipmentByIdAsync(id);
            if (shipmentDto == null) return NotFound();
            return Ok(shipmentDto);
        }

        [HttpPost]
        public async Task<ActionResult<ShipmentDto>> AddShipment([FromBody] ShipmentCreateDto shipmentDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var shipmentDtoResult = await _shipmentService.AddShipmentAsync(shipmentDto);
            return CreatedAtAction("GetShipment", new { id = shipmentDtoResult.Id }, shipmentDtoResult);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateShipment(int id, [FromBody] ShipmentUpdateDto shipmentDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var updatedShipment = await _shipmentService.UpdateShipmentAsync(id, shipmentDto);
            if (updatedShipment == null) return NotFound();

            return Ok(updatedShipment); // artık DTO dönüyor
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
