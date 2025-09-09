using ARMForge.Business.Interfaces;
using ARMForge.Types.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ARMForge.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShipmentProcessController : ControllerBase
    {
        private readonly IShipmentProcessService _shipmentProcessService;

        public ShipmentProcessController(IShipmentProcessService shipmentProcessService)
        {
            _shipmentProcessService = shipmentProcessService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateShipment([FromBody] CreateShipmentRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var newShipment = await _shipmentProcessService.CreateShipmentAsync(request);
                return CreatedAtAction("GetShipment", "Shipments", new { id = newShipment.Id }, newShipment);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, "Sevkiyat oluşturulurken bir hata oluştu.");
            }
        }
    }
}
