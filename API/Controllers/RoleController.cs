using Microsoft.AspNetCore.Mvc;
using Core.Interfaces.Services;

namespace API.Controllers
{
    [Route("api/role")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        // POST: api/role
        [HttpPost]
        public async Task<ActionResult<RoleServiceResponse>> AddNewRole([FromBody] string roleName)
        {
            if (string.IsNullOrWhiteSpace(roleName))
            {
                return BadRequest("Role name is required.");
            }

            var result = await _roleService.AddNewRoleAsync(roleName);
            return CreatedAtAction(nameof(GetRoleById), new { id = result.RoleId }, result);
        }

        // DELETE: api/role/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult<RoleServiceResponse>> DeleteRole(int id)
        {
            try
            {
                var result = await _roleService.DeleteRoleAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        // GET: api/role
        [HttpGet]
        public async Task<ActionResult<List<RoleServiceResponse>>> GetAllRoles()
        {
            var results = await _roleService.GetAllRoleAsync();
            return Ok(results);
        }

        // GET: api/role/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<RoleServiceResponse>> GetRoleById(int id)
        {
            try
            {
                var result = await _roleService.GetRoleByIdAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        // PUT: api/role/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<RoleServiceResponse>> UpdateRole(int id, [FromBody] string roleName)
        {
            if (string.IsNullOrWhiteSpace(roleName))
            {
                return BadRequest("Role name is required.");
            }

            try
            {
                var result = await _roleService.UpdateRoleAsync(id, roleName);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }

}