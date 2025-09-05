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
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        // Tüm rolleri getiren endpoint
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Role>>> Get()
        {
            var roles = await _roleService.GetAllRolesAsync();
            return Ok(roles);
        }

        // ID'ye göre tek bir rol getiren endpoint
        [HttpGet("{id}")]
        public async Task<ActionResult<Role>> Get(int id)
        {
            var role = await _roleService.GetRoleByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }
            return Ok(role);
        }

        // Yeni bir rol ekleyen endpoint
        [HttpPost]
        public async Task<ActionResult<Role>> Post([FromBody] Role role)
        {
            var addedRole = await _roleService.AddRoleAsync(role);
            return CreatedAtAction(nameof(Get), new { id = addedRole.Id }, addedRole);
        }

        // Bir rolü güncelleyen endpoint
        [HttpPut("{id}")]
        public async Task<ActionResult<Role>> Put(int id, [FromBody] Role role)
        {
            if (id != role.Id)
            {
                return BadRequest();
            }

            await _roleService.UpdateRoleAsync(role);
            return Ok(role);
        }

        // Bir rolü silen endpoint
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _roleService.DeleteRoleAsync(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
