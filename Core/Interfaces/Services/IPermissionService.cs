namespace Core.Interfaces.Services;

public interface IPermissionService
{
    Task<PermissionServicePermissionResponse> AddNewPermissionAsync(string PermissionName);
    Task<PermissionServicePermissionResponse> GetPermissionByIdAsync(int PermissionId);
    Task<List<PermissionServicePermissionResponse>> GetAllPermissionAsync();
    Task<PermissionServicePermissionResponse> UpdatePermissionAsync(int PermissionId, string PermissionName);
    Task<PermissionServicePermissionResponse> DeletePermissionAsync(int PermissionId);
}

public class PermissionServicePermissionResponse
{
    public required string PermissionId { get; set; } 
    public required string PermissionName { get; set; } 

}