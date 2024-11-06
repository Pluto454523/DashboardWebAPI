using Microsoft.AspNetCore.Mvc;
using Core.Interfaces.Services;

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
        public async Task<ActionResult<UserServiceResponse>> AddUser([FromBody] UserServiceUserInput input)
        {
            if (input == null)
            {
                return BadRequest("Invalid user data.");
            }

            var result = await _userService.AddNewUserAsync(input);
            // return CreatedAtAction(nameof(GetUserById), new { userId = result.UserId }, result);
            return result;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserServiceResponse>> GetUserById(int id)
        {
            try
            {
                var result = await _userService.GetUserByIdAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<UserServiceResponse>> DeleteUser(int id)
        {
            try
            {
                var result = await _userService.DeleteUserAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<UserServiceResponse>> UpdateUser(int id, [FromBody] UserServiceUserInput input)
        {
            var result = await _userService.UpdateUserAsync(id, input);

            return result;
        }

        [HttpPost("datatable")]
        public async Task<ActionResult<UserServiceResponseWithPaging>> GetAllUsers([FromBody] UserQueryParams queryParams)
        {
            var result = await _userService.GetAllUsersAsync(queryParams);
            return Ok(result);
        }


    }
}
