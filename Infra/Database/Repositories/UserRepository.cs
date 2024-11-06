using Domain;
using Microsoft.EntityFrameworkCore;
using Infra.Database.DatabaseContext;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;

namespace Infra.Database.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly SqlServerDbContext _context;

        public UserRepository(SqlServerDbContext context)
        {
            _context = context;
        }

        public async Task<User> AddUserAsync(User user)
        {
            user.CreatedTime = DateTime.Now;
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<UserPageResult> GetAllUsersAsync(UserQueryParams queryParams)
        {
            var query = _context.Users
                .Include(u => u.UserPermissions) // รวมข้อมูล Permissions ของผู้ใช้
                .ThenInclude(up => up.Permission) // รวมรายละเอียดของ Permission แต่ละรายการ
                .Include(u => u.Role) // รวมข้อมูล Role ของผู้ใช้
                .AsQueryable();

            // Apply search
            if (!string.IsNullOrEmpty(queryParams.Search))
            {
                query = query.Where(u => u.FirstName.Contains(queryParams.Search) ||
                            u.LastName.Contains(queryParams.Search) ||
                            u.Email.Contains(queryParams.Search) ||
                            u.Username.Contains(queryParams.Search)
                        );
            }

            // Apply ordering
            if (!string.IsNullOrEmpty(queryParams.OrderBy))
            {
                query = queryParams.OrderBy switch
                {
                    "FirstName" => queryParams.OrderDirection == "desc" ? query.OrderByDescending(u => u.FirstName) : query.OrderBy(u => u.FirstName),
                    "LastName" => queryParams.OrderDirection == "desc" ? query.OrderByDescending(u => u.LastName) : query.OrderBy(u => u.LastName),
                    _ => query // Default case if OrderBy is not recognized
                };
            }

            // Calculate total count for pagination
            int totalCount = await query.CountAsync();

            // Apply pagination
            int pageSize = queryParams.PageSize ?? 10;
            int pageNumber = queryParams.PageNumber ?? 1;

            var users = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new UserPageResult
            {
                Items = users,
                TotalCount = totalCount,
                PageSize = pageSize,
                PageNumber = pageNumber
            };
        }

        public async Task<User> GetUserByIdAsync(int userId)
        {
            return await _context.Users
                .Include(u => u.UserPermissions)
                .ThenInclude(up => up.Permission)
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.UserId == userId);
        }

        public async Task<User> UpdateUserAsync(User user)
        {
            // var todoList = await _context.Users.FirstOrDefaultAsync(f => f.ToDoListId == toDoListId);
            // if (todoList is null)
            //     return null;
            // var updateUser = 
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task DeleteUserAsync(int userId)
        {
            var user = await GetUserByIdAsync(userId);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<UserPermission> AddUserPermissionAsync(UserPermission userPermission)
        {
            userPermission.CreatedTime = DateTime.Now;
            await _context.UserPermissions.AddAsync(userPermission);
            await _context.SaveChangesAsync();
            return userPermission;
        }

        public async Task<UserPermission> UpdateUserPermissionAsync(UserPermission userPermission)
        {
            // Find the existing permission in the database
            var existingPermission = await _context.UserPermissions
                .FirstOrDefaultAsync(up => up.UserId == userPermission.UserId && up.PermissionId == userPermission.PermissionId);

            if (existingPermission is null)
            {
                userPermission.CreatedTime = DateTime.Now;
                await _context.UserPermissions.AddAsync(userPermission);
            }
            else
            {
                // Update the permission fields
                existingPermission.UpdatedTime = DateTime.Now;
                existingPermission.IsReadable = userPermission.IsReadable;
                existingPermission.IsWritable = userPermission.IsWritable;
                existingPermission.IsDeletable = userPermission.IsDeletable;
            }

            // Save changes to the database
            await _context.SaveChangesAsync();
            return existingPermission;
        }


    }
}
