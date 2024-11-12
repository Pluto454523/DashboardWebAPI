using Domain;
using Core.Interfaces.Services;
using Core.Interfaces.Repositories;
using Core.Models;

namespace Core.Services;

public class RoleService : IRoleService
{
    private readonly IRoleRepository _roleRepository;

    public RoleService(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    public async Task<RoleServiceResponse> AddNewRoleAsync(string roleName)
    {
        if (string.IsNullOrWhiteSpace(roleName))
        {
            throw new ArgumentException("Role name is required.");
        }

        // Create a new Role entity
        var role = new Role
        {
            RoleName = roleName
        };

        // Add the role using the repository
        var createdRole = await _roleRepository.AddRoleAsync(role);

        // Create and return the response
        return new RoleServiceResponse
        {
            RoleId = createdRole.RoleId.ToString(),
            RoleName = createdRole.RoleName
        };
    }

    public async Task<RoleServiceResponse> DeleteRoleAsync(int roleId)
    {
        // Check if the role exists before deleting
        var role = await _roleRepository.GetRoleByIdAsync(roleId);
        if (role == null)
        {
            throw new ArgumentException("Role not found");
        }

        // Delete the role
        await _roleRepository.DeleteRoleAsync(roleId);

        // Return the deleted role response
        return new RoleServiceResponse
        {
            RoleId = role.RoleId.ToString(),
            RoleName = role.RoleName
        };
    }

    public async Task<List<RoleServiceResponse>> GetAllRoleAsync()
    {
        // Get all roles from the repository
        var roles = await _roleRepository.GetAllRolesAsync();

        // Map the roles to the response model
        return roles.Select(p => new RoleServiceResponse
        {
            RoleId = p.RoleId.ToString(),
            RoleName = p.RoleName
        }).ToList();
    }

    public async Task<RoleServiceResponse> GetRoleByIdAsync(int roleId)
    {
        // Retrieve the role by ID
        var role = await _roleRepository.GetRoleByIdAsync(roleId);
        if (role == null)
        {
            throw new ArgumentException("Role not found");
        }

        // Return the response
        return new RoleServiceResponse
        {
            RoleId = role.RoleId.ToString(),
            RoleName = role.RoleName
        };
    }

    public async Task<RoleServiceResponse> UpdateRoleAsync(int roleId, string roleName)
    {
        if (string.IsNullOrWhiteSpace(roleName))
        {
            throw new ArgumentException("Role name is required.");
        }
        // Check if the role exists
        var role = await _roleRepository.GetRoleByIdAsync(roleId);
        if (role == null)
        {
            throw new ArgumentException("Role not found");
        }

        // Update the role name
        role.RoleName = roleName;
        await _roleRepository.UpdateRoleAsync(role);

        // Return the updated role response
        return new RoleServiceResponse
        {
            RoleId = roleId.ToString(),
            RoleName = roleName
        };
    }
}