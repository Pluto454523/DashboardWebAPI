using Domain;
using Core.Models;

namespace Core.Interfaces.Repositories;

public interface IUserRepository
{

    Task<User> AddUserAsync(User user);
    Task<User> GetUserByIdAsync(int userId);
    Task<List<User>> GetAllUsersAsync(UserQueryParams queryParams);
    Task<User> UpdateUserAsync(User user);
    Task DeleteUserAsync(int userId);
    Task<UserPermission> AddUserPermissionAsync(UserPermission userPermission);
    Task<UserPermission> UpdateUserPermissionAsync(UserPermission userPermission);

}