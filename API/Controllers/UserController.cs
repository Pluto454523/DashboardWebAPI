using Microsoft.AspNetCore.Mvc;
using Core.Interfaces.Services;
using Core.Models;


namespace API.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("add")]
        public async Task<ActionResult<ApiResponse<UserResponse>>> AddUser([FromBody] AddUserRequest input)
        {
            try
            {
                var result = await _userService.AddNewUserAsync(input);
                var response = new ApiResponse<UserResponse>
                {
                    Status = new ApiStatus
                    {
                        Code = 2000,
                        Description = "Success",
                    },
                    Data = result,
                };

                StatusCode(StatusCodes.Status200OK, response);
                return response;
            }
            catch (ArgumentException ex)
            {
                var response = new ApiResponse<UserResponse>
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
        public async Task<ActionResult<ApiResponse<UserResponse>>> GetUserById(int id)
        {
            try
            {
                var result = await _userService.GetUserByIdAsync(id);
                var response = new ApiResponse<UserResponse>
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
                var response = new ApiResponse<UserResponse>
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

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<DeleteUserResponse>>> DeleteUser(int id)
        {
            try
            {
                var result = await _userService.DeleteUserAsync(id);
                var response = new ApiResponse<DeleteUserResponse>
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
                var response = new ApiResponse<UserResponse>
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

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<UserResponse>>> UpdateUser(int id, [FromBody] AddUserRequest input)
        {
            try
            {
                var result = await _userService.UpdateUserAsync(id, input);
                var response = new ApiResponse<UserResponse>
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
                var response = new ApiResponse<UserResponse>
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

        [HttpPost("datatable")]
        public async Task<ActionResult<ApiResponseWithPaging<UserResponse>>> GetAllUsers([FromBody] UserQueryParams queryParams)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ModelState);
            }

            try
            {
                var result = await _userService.GetAllUsersAsync(queryParams);
                return StatusCode(StatusCodes.Status200OK, result);
            }
            catch (ArgumentException ex)
            {
                var StatusResponse = new ApiStatus
                {
                    Code = 4001,
                    Description = ex.Message,
                };
                return StatusCode(StatusCodes.Status400BadRequest, StatusResponse);
            }
        }


    }
}
