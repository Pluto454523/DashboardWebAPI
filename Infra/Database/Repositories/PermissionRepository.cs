using Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Infra.Database.DatabaseContext;
using Core.Interfaces.Repositories;

namespace Infra.Database.Repositories
{
    public class PermissionRepository : IPermissionRepository
    {
        private readonly SqlServerDbContext _context;

        public PermissionRepository(SqlServerDbContext context)
        {
            _context = context;
        }

        public async Task<List<Permission>> GetAllPermissionsAsync()
        {
            return await _context.Permissions.ToListAsync();
        }

        public async Task<Permission> GetPermissionByIdAsync(int permissionId)
        {
            return await _context.Permissions.FindAsync(permissionId);
        }

        public async Task<Permission> AddPermissionAsync(Permission permission)
        {
            await _context.Permissions.AddAsync(permission);
            await _context.SaveChangesAsync();
            return permission;
        }

        public async Task UpdatePermissionAsync(Permission permission)
        {
            _context.Permissions.Update(permission);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePermissionAsync(int permissionId)
        {
            var permission = await GetPermissionByIdAsync(permissionId);
            if (permission != null)
            {
                _context.Permissions.Remove(permission);
                await _context.SaveChangesAsync();
            }
        }


    }
}
