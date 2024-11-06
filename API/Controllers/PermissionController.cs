using Microsoft.AspNetCore.Mvc;
using Core.Interfaces.Services;

namespace API.Controllers
{
    [Route("api/permission")]
    [ApiController]
    public class PermissionController : ControllerBase
    {
        private readonly IPermissionService _permissionService;

        public PermissionController(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        // POST: api/permission
        [HttpPost]
        public async Task<ActionResult<PermissionServicePermissionResponse>> AddNewPermission([FromBody] string permissionName)
        {
            if (string.IsNullOrWhiteSpace(permissionName))
            {
                return BadRequest("Permission name is required.");
            }

            var result = await _permissionService.AddNewPermissionAsync(permissionName);
            return CreatedAtAction(nameof(GetPermissionById), new { id = result.PermissionId }, result);
        }

        // DELETE: api/permission/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult<PermissionServicePermissionResponse>> DeletePermission(int id)
        {
            try
            {
                var result = await _permissionService.DeletePermissionAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        // GET: api/permission
        [HttpGet]
        public async Task<ActionResult<List<PermissionServicePermissionResponse>>> GetAllPermissions()
        {
            var results = await _permissionService.GetAllPermissionAsync();
            return Ok(results);
        }

        // GET: api/permission/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<PermissionServicePermissionResponse>> GetPermissionById(int id)
        {
            try
            {
                var result = await _permissionService.GetPermissionByIdAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        // PUT: api/permission/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<PermissionServicePermissionResponse>> UpdatePermission(int id, [FromBody] string permissionName)
        {
            if (string.IsNullOrWhiteSpace(permissionName))
            {
                return BadRequest("Permission name is required.");
            }

            try
            {
                var result = await _permissionService.UpdatePermissionAsync(id, permissionName);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }

}