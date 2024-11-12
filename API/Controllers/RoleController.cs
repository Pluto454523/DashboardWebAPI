using Microsoft.AspNetCore.Mvc;
using Core.Interfaces.Services;
using Core.Models;

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
        public async Task<ActionResult<ApiResponse<RoleServiceResponse>>> AddNewRole([FromBody] string roleName)
        {
            try
            {
                var result = await _roleService.AddNewRoleAsync(roleName);
                var response = new ApiResponse<RoleServiceResponse>
                {
                    Status = new ApiStatus
                    {
                        Code = 2000,
                        Description = "Success",
                    },
                    Data = result,
                };

                return StatusCode(StatusCodes.Status200OK, response);
            }
            catch (ArgumentException ex)
            {
                var response = new ApiResponse<RoleServiceResponse>
                {
                    Status = new ApiStatus
                    {
                        Code = 4001,
                        Description = ex.Message,
                    },
                    Data = null,
                };

                return StatusCode(StatusCodes.Status400BadRequest, response);
            }

        }

        // DELETE: api/role/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<RoleServiceResponse>>> DeleteRole(int id)
        {
            try
            {
                var result = await _roleService.DeleteRoleAsync(id);
                var response = new ApiResponse<RoleServiceResponse>
                {
                    Status = new ApiStatus
                    {
                        Code = 2000,
                        Description = "Success",
                    },
                    Data = result,
                };

                StatusCode(StatusCodes.Status200OK);
                return response;
            }
            catch (ArgumentException ex)
            {
                var response = new ApiResponse<RoleServiceResponse>
                {
                    Status = new ApiStatus
                    {
                        Code = 4001,
                        Description = ex.Message,
                    },
                    Data = null,
                };

                StatusCode(StatusCodes.Status400BadRequest);
                return response;
            }
        }

        // GET: api/role
        [HttpGet]
        public async Task<ActionResult<ApiResponse<List<RoleServiceResponse>>>> GetAllRoles()
        {
            try
            {
                var result = await _roleService.GetAllRoleAsync();
                var response = new ApiResponse<List<RoleServiceResponse>>
                {
                    Status = new ApiStatus
                    {
                        Code = 2000,
                        Description = "Success",
                    },
                    Data = result,
                };

                StatusCode(StatusCodes.Status200OK);
                return response;
            }
            catch (ArgumentException ex)
            {
                var response = new ApiResponse<List<RoleServiceResponse>>
                {
                    Status = new ApiStatus
                    {
                        Code = 4001,
                        Description = ex.Message,
                    },
                    Data = null,
                };

                StatusCode(StatusCodes.Status400BadRequest);
                return response;
            }
        }

        // GET: api/role/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<RoleServiceResponse>>> GetRoleById(int id)
        {
            try
            {
                var result = await _roleService.GetRoleByIdAsync(id);
                var response = new ApiResponse<RoleServiceResponse>
                {
                    Status = new ApiStatus
                    {
                        Code = 2000,
                        Description = "Success",
                    },
                    Data = result,
                };

                StatusCode(StatusCodes.Status200OK);
                return response;
            }
            catch (ArgumentException ex)
            {
                var response = new ApiResponse<RoleServiceResponse>
                {
                    Status = new ApiStatus
                    {
                        Code = 4001,
                        Description = ex.Message,
                    },
                    Data = null,
                };

                StatusCode(StatusCodes.Status400BadRequest);
                return response;
            }
        }

        // PUT: api/role/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<RoleServiceResponse>>> UpdateRole(int id, [FromBody] string roleName)
        {

            try
            {
                var result = await _roleService.UpdateRoleAsync(id, roleName);
                var response = new ApiResponse<RoleServiceResponse>
                {
                    Status = new ApiStatus
                    {
                        Code = 2000,
                        Description = "Success",
                    },
                    Data = result,
                };

                StatusCode(StatusCodes.Status200OK);
                return response;
            }
            catch (ArgumentException ex)
            {
                var response = new ApiResponse<RoleServiceResponse>
                {
                    Status = new ApiStatus
                    {
                        Code = 4001,
                        Description = ex.Message,
                    },
                    Data = null,
                };

                StatusCode(StatusCodes.Status400BadRequest);
                return response;
            }


        }
    }

}