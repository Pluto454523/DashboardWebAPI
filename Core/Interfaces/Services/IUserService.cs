using Core.Models;

namespace Core.Interfaces.Services;

public interface IUserService
{
    Task<UserResponse> AddNewUserAsync(AddUserRequest input);
    Task<UserResponse> GetUserByIdAsync(int userId);
    Task<UserResponse> UpdateUserAsync(int userId, AddUserRequest input);
    Task<DeleteUserResponse> DeleteUserAsync(int userId);
    Task<ApiResponseWithPaging<UserResponse>> GetAllUsersAsync(UserQueryParams queryParams);
}