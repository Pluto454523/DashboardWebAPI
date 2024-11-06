using Domain;

namespace Core.Interfaces.Repositories;

public interface IRoleRepository
{
    Task<List<Role>> GetAllRolesAsync();
    Task<Role> GetRoleByIdAsync(int roleId);
    Task<Role> AddRoleAsync(Role role);
    Task UpdateRoleAsync(Role role);
    Task DeleteRoleAsync(int roleId);

}