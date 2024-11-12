using Microsoft.AspNetCore.Mvc;
using Core.Interfaces.Services;
using Core.Models;

namespace API.Controllers
{
    [Route("api/v1/permissions")]
    [ApiController]
    public class PermissionController : ControllerBase
    {
        private readonly IPermissionService _permissionService;

        public PermissionController(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<PermissionServicePermissionResponse>>> AddNewPermission([FromBody] string permissionName)
        {
            try
            {
                var result = await _permissionService.AddNewPermissionAsync(permissionName);
                var response = new ApiResponse<PermissionServicePermissionResponse>
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
                var response = new ApiResponse<PermissionServicePermissionResponse>
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

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<PermissionServicePermissionResponse>>> DeletePermission(int id)
        {

            try
            {
                var result = await _permissionService.DeletePermissionAsync(id);
                var response = new ApiResponse<PermissionServicePermissionResponse>
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
                var response = new ApiResponse<PermissionServicePermissionResponse>
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

        [HttpGet]
        public async Task<ActionResult<ApiResponse<List<PermissionServicePermissionResponse>>>> GetAllPermissions()
        {
            try
            {
                var result = await _permissionService.GetAllPermissionAsync();
                var response = new ApiResponse<List<PermissionServicePermissionResponse>>
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
                var response = new ApiResponse<List<PermissionServicePermissionResponse>>
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

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<PermissionServicePermissionResponse>>> GetPermissionById(int id)
        {
            try
            {
                var result = await _permissionService.GetPermissionByIdAsync(id);
                var response = new ApiResponse<PermissionServicePermissionResponse>
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
                var response = new ApiResponse<PermissionServicePermissionResponse>
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

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<PermissionServicePermissionResponse>>> UpdatePermission(int id, [FromBody] string permissionName)
        {
            try
            {
                var result = await _permissionService.UpdatePermissionAsync(id, permissionName);
                var response = new ApiResponse<PermissionServicePermissionResponse>
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
                var response = new ApiResponse<PermissionServicePermissionResponse>
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