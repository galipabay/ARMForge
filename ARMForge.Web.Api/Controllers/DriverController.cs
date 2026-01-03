using ARMForge.Business.Interfaces;
using ARMForge.Infrastructure;
using ARMForge.Kernel.Entities;
using ARMForge.Types.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ARMForge.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DriverController : ControllerBase
    {
        private readonly IDriverService _driverService;
        private readonly IUserService _userService;

        public DriverController(IDriverService driverService, IUserService userService)
        {
            _driverService = driverService;
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DriverDto>>> GetAllDrivers()
        {
            var drivers = await _driverService.GetAllDriversAsync();
            return Ok(drivers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DriverDto>> GetDriver(int id)
        {
            var driverDto = await _driverService.GetDriverByIdAsync(id);
            if (driverDto == null) return NotFound();
            return Ok(driverDto);
        }

        [HttpPost]
        public async Task<ActionResult<DriverDto>> AddDriver([FromBody] DriverCreateDto driverDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // ✅ User check'i ModelState ile birleştir
            var userId_check = await _userService.CheckUserById(driverDto.UserId);
            if (!userId_check)
            {
                ModelState.AddModelError("UserId", "The specified UserId does not exist.");
                return BadRequest(ModelState);
            }

            try
            {
                var addedDriver = await _driverService.AddDriverAsync(driverDto);
                return CreatedAtAction(nameof(GetDriver), new { id = addedDriver.Id }, addedDriver);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }

        }

        [HttpPut("{id}")]
        public async Task<ActionResult<DriverDto>> UpdateDriver(int id, [FromBody] DriverUpdateDto driverUpdateDto) // ✅ DTO
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var updatedDriver = await _driverService.UpdateDriverAsync(id, driverUpdateDto);
                return Ok(updatedDriver);
            }
            catch (InvalidOperationException ex) // ✅ Exception handling
            {
                return NotFound(new { error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDriver(int id)
        {
            var result = await _driverService.DeleteDriverAsync(id);
            if (!result) return NotFound();

            return NoContent();
        }
    }
}
