using Microsoft.EntityFrameworkCore;
using Infra.Database.Configurations;
using Domain;
using System.Reflection;

namespace Infra.Database.DatabaseContext
{
    public class SqlServerDbContext : DbContext
    {
        public SqlServerDbContext(DbContextOptions<SqlServerDbContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<UserPermission> UserPermissions { get; set; }


        // protected override void OnModelCreating(ModelBuilder modelBuilder)
        // {
        //     base.OnModelCreating(modelBuilder);

        //     // นำ Configuration แต่ละ Entity มาใช้
        //     modelBuilder.ApplyConfiguration(new UserConfiguration());
        //     modelBuilder.ApplyConfiguration(new RoleConfiguration());
        //     modelBuilder.ApplyConfiguration(new PermissionConfiguration());
        //     modelBuilder.ApplyConfiguration(new UserPermissionConfiguration());

        // }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }


    }
}
