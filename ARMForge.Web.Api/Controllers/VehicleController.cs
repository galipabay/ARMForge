using ARMForge.Business.Interfaces;
using ARMForge.Kernel.Entities;
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
        public async Task<IActionResult> GetAllVehicles()
        {
            var vehicles = await _vehicleService.GetAllVehiclesAsync();
            return Ok(vehicles);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetVehicleById(int id)
        {
            var vehicle = await _vehicleService.GetVehicleByIdAsync(id);
            if (vehicle == null)
            {
                return NotFound();
            }
            return Ok(vehicle);
        }
        [HttpPost]
        public async Task<IActionResult> AddVehicle([FromBody] Vehicle vehicle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var createdVehicle = await _vehicleService.AddVehicleAsync(vehicle);
            return CreatedAtAction(nameof(GetVehicleById), new { id = createdVehicle.Id }, createdVehicle);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVehicle(int id, [FromBody] Vehicle vehicle)
        {
            if (id != vehicle.Id || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var updatedVehicle = await _vehicleService.UpdateVehicleAsync(vehicle);
            if (updatedVehicle == null)
            {
                return NotFound();
            }
            return Ok(updatedVehicle);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicle(int id)
        {
            var result = await _vehicleService.DeleteVehicleAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
