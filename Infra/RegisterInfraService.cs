using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Infra.Database.DatabaseContext;
using Infra.Database.Repositories;
using Core.Interfaces.Repositories;
using Core;

namespace Infra
{
    public static class RegisterInfraService
    {
        public static IServiceCollection AddInfraService(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            // ตั้งค่า DbContext
            services.AddDbContext<SqlServerDbContext>(options => options.UseSqlServer(connectionString));

            // ตัวอย่างการเพิ่ม Logging
            // services.AddLogging();

            // ตัวอย่างการเพิ่ม Caching (ถ้ามี)
            // services.AddMemoryCache();

            // เพิ่มบริการอื่นๆ เช่น การเชื่อมต่อ API ภายนอก, Service เฉพาะด้าน ฯลฯ
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPermissionRepository, PermissionRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();


            
            // เพิ่มบริการเพิ่มเติมที่จำเป็น

            return services;
        }
    }
}


// อธิบายโค้ด
// 1. AddDbContext: เพิ่ม ApplicationDbContext ใน DI Container โดยใช้ UseSqlServer กับค่า Connection String ที่ได้จาก IConfiguration
// 2. AddLogging: เพิ่มบริการ Logging เพื่อให้สามารถใช้ Logging ในแอปพลิเคชันได้
// 3. AddMemoryCache: เพิ่ม Caching ภายในแอปพลิเคชันด้วย Memory Cache
// 4. AddScoped: คุณสามารถเพิ่มบริการอื่นๆ โดยการเรียกใช้ AddScoped, AddSingleton, หรือ AddTransient เพื่อกำหนดอายุการใช้งานของบริการที่ต้องการลงทะเบียน   