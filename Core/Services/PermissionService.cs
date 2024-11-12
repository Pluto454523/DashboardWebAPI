using Domain;
using Core.Interfaces.Services;
using Core.Interfaces.Repositories;
using Core.Models;

namespace Core.Services;

public class PermissionService : IPermissionService
{
    private readonly IPermissionRepository _permissionRepository;

    public PermissionService(IPermissionRepository permissionRepository)
    {
        _permissionRepository = permissionRepository;
    }

    public async Task<PermissionServicePermissionResponse> AddNewPermissionAsync(string permissionName)
    {
        if (string.IsNullOrWhiteSpace(permissionName))
        {
            throw new ArgumentException("Permission name is required.");
        }
        // Create a new Permission entity
        var permission = new Permission
        {
            PermissionName = permissionName
        };

        // Add the permission using the repository
        var createdPermission = await _permissionRepository.AddPermissionAsync(permission);

        // Create and return the response
        return new PermissionServicePermissionResponse
        {
            PermissionId = createdPermission.PermissionId.ToString(),
            PermissionName = createdPermission.PermissionName
        };
    }

    public async Task<PermissionServicePermissionResponse> DeletePermissionAsync(int permissionId)
    {
        // Check if the permission exists before deleting
        var permission = await _permissionRepository.GetPermissionByIdAsync(permissionId);
        if (permission == null)
        {
            throw new ArgumentException("Permission not found");
        }

        // Delete the permission
        await _permissionRepository.DeletePermissionAsync(permissionId);

        // Return the deleted permission response
        return new PermissionServicePermissionResponse
        {
            PermissionId = permission.PermissionId.ToString(),
            PermissionName = permission.PermissionName
        };
    }

    public async Task<List<PermissionServicePermissionResponse>> GetAllPermissionAsync()
    {
        // Get all permissions from the repository
        var permissions = await _permissionRepository.GetAllPermissionsAsync();

        // Map the permissions to the response model
        return permissions.Select(p => new PermissionServicePermissionResponse
        {
            PermissionId = p.PermissionId.ToString(),
            PermissionName = p.PermissionName
        }).ToList();
    }

    public async Task<PermissionServicePermissionResponse> GetPermissionByIdAsync(int permissionId)
    {
        // Retrieve the permission by ID
        var permission = await _permissionRepository.GetPermissionByIdAsync(permissionId);
        if (permission == null)
        {
            throw new ArgumentException("Permission not found");
        }

        // Return the response
        return new PermissionServicePermissionResponse
        {
            PermissionId = permission.PermissionId.ToString(),
            PermissionName = permission.PermissionName
        };
    }

    public async Task<PermissionServicePermissionResponse> UpdatePermissionAsync(int permissionId, string permissionName)
    {
        if (string.IsNullOrWhiteSpace(permissionName))
        {
            throw new ArgumentException("Permission name is required.");
        }

        // Check if the permission exists
        var permission = await _permissionRepository.GetPermissionByIdAsync(permissionId);
        if (permission == null)
        {
            throw new ArgumentException("Permission not found");
        }

        // Update the permission name
        permission.PermissionName = permissionName;
        await _permissionRepository.UpdatePermissionAsync(permission);

        // Return the updated permission response
        return new PermissionServicePermissionResponse
        {
            PermissionId = permissionId.ToString(),
            PermissionName = permissionName
        };
    }
}