using System.ComponentModel.DataAnnotations;

namespace Core.Models;

public class UserQueryParams
{
    public string? OrderBy { get; set; }
    
    [RegularExpression("asc|desc", ErrorMessage = "OrderDirection must be 'asc' or 'desc'.")]
    public string? OrderDirection { get; set; } = "asc"; // default to ascending

    [Range(1, int.MaxValue, ErrorMessage = "PageNumber must be greater than 0.")]
    public int? PageNumber { get; set; } = 1; // default to first page

    [Range(1, 100, ErrorMessage = "PageSize must be between 1 and 100.")]
    public int? PageSize { get; set; } = 10; // default page size

    [StringLength(50, ErrorMessage = "Search term cannot exceed 50 characters.")]
    public string? Search { get; set; }
}


public class PermissionInput
{
    public required string PermissionId { get; set; }
    public bool IsReadable { get; set; }
    public bool IsWritable { get; set; }
    public bool IsDeletable { get; set; }
}

public class AddUserRequest
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public string? Phone { get; set; }
    public required string RoleId { get; set; }
    public required string Username { get; set; }
    public required string Password { get; set; }
    public required List<PermissionInput> Permissions { get; set; }
}