namespace Core.Models;

public class PermissionResponse
{
    public required string PermissionId { get; set; }
    public required string PermissionName { get; set; }

}

public class RoleResponse
{
    public required string RoleId { get; set; }
    public required string RoleName { get; set; }

}

public class UserResponse
{
    public required string UserId { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public string? Phone { get; set; }
    public required string Username { get; set; }
    public required List<PermissionResponse> Permissions { get; set; }
    public required RoleResponse Role { get; set; }

}

public class DeleteUserResponse
{
    public required bool result { get; set; }
    public required string message { get; set; }

}