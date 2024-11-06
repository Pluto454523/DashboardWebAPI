using Microsoft.Extensions.DependencyInjection;
using Core.Interfaces.Services;
using Core.Services;

namespace Core
{
    public static class RegisterCoreService
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services)
        {
            // Register UserService
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPermissionService, PermissionService>();
            services.AddScoped<IRoleService, RoleService>();
            
            return services;
        }
    }
}
