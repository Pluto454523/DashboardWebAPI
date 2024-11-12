using Core.Models;

namespace Core.Interfaces.Services;

public interface IRoleService
{
    Task<RoleServiceResponse> AddNewRoleAsync(string RoleName);
    Task<RoleServiceResponse> GetRoleByIdAsync(int RoleId);
    Task<List<RoleServiceResponse>> GetAllRoleAsync();
    Task<RoleServiceResponse> UpdateRoleAsync(int RoleId, string RoleName);
    Task<RoleServiceResponse> DeleteRoleAsync(int RoleId);
}