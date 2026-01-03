using ARMForge.Business.Interfaces;
using ARMForge.Kernel.Entities;
using ARMForge.Types.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ARMForge.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private readonly IVehicleService _vehicleService;
        public VehicleController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VehicleDto>>> GetAllVehicles()
        {
            var vehicles = await _vehicleService.GetAllVehiclesAsync();
            return Ok(vehicles);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<VehicleDto>> GetVehicleById(int id)
        {
            var vehicle = await _vehicleService.GetVehicleByIdAsync(id);
            return vehicle == null ? NotFound() : Ok(vehicle);
        }

        [HttpPost]
        public async Task<ActionResult<VehicleDto>> AddVehicle([FromBody] VehicleCreateDto vehicleDto) // ✅ FIXED
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var addedVehicle = await _vehicleService.AddVehicleAsync(vehicleDto);
                return CreatedAtAction(nameof(GetVehicleById), new { id = addedVehicle.Id }, addedVehicle);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<VehicleDto>> UpdateVehicle(int id, [FromBody] VehicleUpdateDto vehicleDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var updatedVehicle = await _vehicleService.UpdateVehicleAsync(id, vehicleDto);
                return Ok(updatedVehicle);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicle(int id)
        {
            var result = await _vehicleService.DeleteVehicleAsync(id);
            return result ? NoContent() : NotFound();
        }
    }
}
