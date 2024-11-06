namespace Core.Interfaces.Services;

public interface IUserService
{
    Task<UserServiceResponse> AddNewUserAsync(UserServiceUserInput input);
    Task<UserServiceResponse> GetUserByIdAsync(int userId);
    Task<UserServiceResponse> UpdateUserAsync(int userId, UserServiceUserInput input);
    Task<UserServiceDeleteResponse> DeleteUserAsync(int userId);
    Task<UserServiceResponseWithPaging> GetAllUsersAsync(UserQueryParams queryParams);
}

public class UserQueryParams
{
    public string? OrderBy { get; set; }
    public string? OrderDirection { get; set; } = "asc"; // default to ascending
    public int? PageNumber { get; set; } = 1; // default to first page
    public int? PageSize { get; set; } = 10; // default page size
    public string? Search { get; set; }
}

public class UserServiceResponseWithPaging
{
    public List<UserServiceResponse> DataSource { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }
}

public class UserServicePermissionInput
{
    public required string PermissionId { get; set; } 
    public bool IsReadable { get; set; }
    public bool IsWritable { get; set; }
    public bool IsDeletable { get; set; }
}

public class UserServiceUserInput
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public string? Phone { get; set; } 
    public required string RoleId { get; set; }
    public required string Username { get; set; } 
    public required string Password { get; set; }
    public required List<UserServicePermissionInput> Permissions { get; set; }
}

public class UserServicePermissionResponse
{
    public required string PermissionId { get; set; }
    public required string PermissionName { get; set; }

}

public class UserServiceRoleResponse
{
    public required string RoleId { get; set; }    
    public required string RoleName { get; set; }

}

public class UserServiceResponse
{
    public required string UserId { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public string? Phone { get; set; }
    public required string Username { get; set; }
    public required List<UserServicePermissionResponse> Permissions { get; set; }
    public required UserServiceRoleResponse Role { get; set; }

}

public class UserServiceDeleteResponse
{
    public required bool result { get; set; }
    public required string message { get; set; }

}

// public class UserServiceResponseWithPaging
// {
//     public string OrderBy { get; set; }
//     public string OrderDirection { get; set; }
//     public int PageNumber { get; set; }
//     public int PageSize { get; set; }
//     public string Search { get; set; }
//     public List<UserServiceResponse> Users { get; set; }
// }