using Domain;
using Core.Interfaces.Services;

namespace Core.Interfaces.Repositories;

public interface IUserRepository
{

    Task<User> AddUserAsync(User user);
    Task<User> GetUserByIdAsync(int userId);
    Task<UserPageResult> GetAllUsersAsync(UserQueryParams queryParams);
    Task<User> UpdateUserAsync(User user);
    Task DeleteUserAsync(int userId);
    Task<UserPermission> AddUserPermissionAsync(UserPermission userPermission);
    Task<UserPermission> UpdateUserPermissionAsync(UserPermission userPermission);

}

public class UserPageResult
{
    public List<User> Items { get; set; }
    public int TotalCount { get; set; }
    public int PageSize { get; set; }
    public int PageNumber { get; set; }
}