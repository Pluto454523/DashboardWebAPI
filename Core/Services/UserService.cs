using Domain;
using Core.Interfaces.Services;
using Core.Interfaces.Repositories;
using Core.Models;

namespace Core.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    private readonly IPermissionRepository _permissionRepository;

    private readonly IRoleRepository _roleRepository;

    public UserService(IUserRepository userRepository, IPermissionRepository permissionRepository, IRoleRepository roleRepository)
    {
        _userRepository = userRepository;
        _permissionRepository = permissionRepository;
        _roleRepository = roleRepository;
    }

    // Add a new user
    public async Task<UserResponse> AddNewUserAsync(AddUserRequest input)
    {
        // Validate and parse RoleId
        if (!int.TryParse(input.RoleId, out int roleId))
        {
            throw new ArgumentException("Bad request. Role ID is invalid");
        }

        var role = await _roleRepository.GetRoleByIdAsync(roleId);
        if (role is null)
        {
            throw new ArgumentException("Bad request. Role not found");
        }

        // Validate permissions
        var userPermissions = new List<UserPermission>();
        foreach (var permissionInput in input.Permissions)
        {
            if (!int.TryParse(permissionInput.PermissionId, out int permissionId))
            {
                throw new ArgumentException("Bad request. Permission ID is invalid");
            }

            var permission = await _permissionRepository.GetPermissionByIdAsync(permissionId);
            if (permission is null)
            {
                throw new ArgumentException($"Bad request. Permission ID {permissionId} not found");
            }

            // Validate permission input fields
            if (!permissionInput.IsReadable && !permissionInput.IsWritable && !permissionInput.IsDeletable)
            {
                throw new ArgumentException("Bad request. At least one permission must be granted.");
            }

            // Add valid permissions to the list
            userPermissions.Add(new UserPermission
            {
                PermissionId = permissionId,
                IsReadable = permissionInput.IsReadable,
                IsWritable = permissionInput.IsWritable,
                IsDeletable = permissionInput.IsDeletable
            });
        }

        // Create the new user after all validations are passed
        var createdUser = await _userRepository.AddUserAsync(new User
        {
            FirstName = input.FirstName,
            LastName = input.LastName,
            Email = input.Email,
            Phone = input.Phone,
            Username = input.Username,
            Password = input.Password,
            RoleId = roleId
        });

        // Add permissions for the new user
        foreach (var userPermission in userPermissions)
        {
            userPermission.UserId = createdUser.UserId; // Set the UserId to the created user
            await _userRepository.AddUserPermissionAsync(userPermission);
        }

        // Prepare the response
        var userResponse = new UserResponse
        {
            UserId = createdUser.UserId.ToString(),
            FirstName = createdUser.FirstName,
            LastName = createdUser.LastName,
            Email = createdUser.Email,
            Phone = createdUser.Phone,
            Username = createdUser.Username,
            Role = new RoleResponse
            {
                RoleId = role.RoleId.ToString(),
                RoleName = role.RoleName,
            },
            Permissions = userPermissions.Select(up => new PermissionResponse
            {
                PermissionId = up.PermissionId.ToString(),
                PermissionName = up.Permission.PermissionName
            }).ToList()
        };

        return userResponse;
    }

    // Get user by id
    public async Task<UserResponse> GetUserByIdAsync(int userId)
    {
        var user = await _userRepository.GetUserByIdAsync(userId);
        if (user is null) throw new ArgumentException("Bad request. User not found");

        var response = new UserResponse
        {
            UserId = user.UserId.ToString(),
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Phone = user.Phone,
            Username = user.Username,
            Role = new RoleResponse
            {
                RoleId = user.Role.RoleId.ToString(),
                RoleName = user.Role.RoleName
            },
            Permissions = user.UserPermissions.Select(up => new PermissionResponse
            {
                PermissionId = up.Permission.PermissionId.ToString(),
                PermissionName = up.Permission.PermissionName
            }).ToList()
        };

        return response;
    }

    // Delete user
    public async Task<DeleteUserResponse> DeleteUserAsync(int userId)
    {
        var user = await _userRepository.GetUserByIdAsync(userId);
        if (user is null) throw new ArgumentException("Bad request. User not found");

        await _userRepository.DeleteUserAsync(userId);
        return new DeleteUserResponse
        {
            result = true,
            message = $"deleted user id {userId}"
        };
    }

    // Update user
    public async Task<UserResponse> UpdateUserAsync(int userId, AddUserRequest input)
    {
        var user = await _userRepository.GetUserByIdAsync(userId);
        if (user is null) throw new ArgumentException("Bad request. User not found");

        // Validate and parse RoleId
        if (!int.TryParse(input.RoleId, out int roleId))
        {
            throw new ArgumentException("Bad request. Role ID is invalid");
        }

        var role = await _roleRepository.GetRoleByIdAsync(roleId);
        if (role is null)
        {
            throw new ArgumentException("Bad request. Role not found");
        }

        // Validate permissions
        var userPermissions = new List<UserPermission>();
        foreach (var permissionInput in input.Permissions)
        {
            if (!int.TryParse(permissionInput.PermissionId, out int permissionId))
            {
                throw new ArgumentException("Bad request. Permission ID is invalid");
            }

            var foundPermission = await _permissionRepository.GetPermissionByIdAsync(permissionId);
            if (foundPermission is null)
            {
                throw new ArgumentException($"Bad request. Permission ID {permissionId} not found");
            }

            // Validate permission input fields
            if (!permissionInput.IsReadable && !permissionInput.IsWritable && !permissionInput.IsDeletable)
            {
                throw new ArgumentException("Bad request. At least one permission must be granted.");
            }

            // Add valid permissions to the list
            userPermissions.Add(new UserPermission
            {
                PermissionId = permissionId,
                IsReadable = permissionInput.IsReadable,
                IsWritable = permissionInput.IsWritable,
                IsDeletable = permissionInput.IsDeletable
            });
        }

        // Update user field
        user.FirstName = input.FirstName;
        user.LastName = input.LastName;
        user.Email = input.Email;
        user.Phone = input.Phone;
        user.Username = input.Username;
        user.Password = input.Password;
        user.RoleId = roleId;

        var UpdatedUser = await _userRepository.UpdateUserAsync(user);

        // update permissions for user
        foreach (var userPermission in userPermissions)
        {
            userPermission.UserId = userId;
            await _userRepository.UpdateUserPermissionAsync(userPermission);
        }

        return await this.GetUserByIdAsync(userId);

    }

    public async Task<ApiResponseWithPaging<UserResponse>> GetAllUsersAsync(UserQueryParams queryParams)
    {

        // Fetch data from repository
        var users = await _userRepository.GetAllUsersAsync(queryParams);

        // Transform users to response model
        var userResponses = users.Select(user => new UserResponse
        {
            UserId = user.UserId.ToString(),
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Role = new RoleResponse
            {
                RoleId = user.Role.RoleId.ToString(),
                RoleName = user.Role.RoleName
            },
            Username = user.Username,
            Permissions = user.UserPermissions.Select(up => new PermissionResponse
            {
                PermissionId = up.Permission.PermissionId.ToString(),
                PermissionName = up.Permission.PermissionName
            }).ToList(),
        }).ToList();

        // Return paginated response
        return new ApiResponseWithPaging<UserResponse>
        {
            DataSource = userResponses,
            Page = queryParams.PageNumber ?? 1,
            PageSize = queryParams.PageSize ?? 10,
            TotalCount = users.Count()
        };
    }

}