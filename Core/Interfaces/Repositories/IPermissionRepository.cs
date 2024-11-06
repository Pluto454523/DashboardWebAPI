using Domain;

namespace Core.Interfaces.Repositories;

public interface IPermissionRepository
{
    Task<List<Permission>> GetAllPermissionsAsync();
    Task<Permission> GetPermissionByIdAsync(int permissionId);
    Task<Permission> AddPermissionAsync(Permission permission);
    Task UpdatePermissionAsync(Permission permission);
    Task DeletePermissionAsync(int permissionId);

}