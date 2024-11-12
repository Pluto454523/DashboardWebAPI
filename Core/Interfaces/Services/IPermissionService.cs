using Core.Models;

namespace Core.Interfaces.Services;

public interface IPermissionService
{
    Task<PermissionServicePermissionResponse> AddNewPermissionAsync(string PermissionName);
    Task<PermissionServicePermissionResponse> GetPermissionByIdAsync(int PermissionId);
    Task<List<PermissionServicePermissionResponse>> GetAllPermissionAsync();
    Task<PermissionServicePermissionResponse> UpdatePermissionAsync(int PermissionId, string PermissionName);
    Task<PermissionServicePermissionResponse> DeletePermissionAsync(int PermissionId);
}