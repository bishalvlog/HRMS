using HRMS.Core.Interfaces.Menu;
using HRMS.Core.Interfaces.Users;
using HRMS.Data.DataSeeder;
using HRMS.Data.Repository.Users;

namespace HRMS
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var loggerFactory = services.GetRequiredService<ILoggerFactory>();
                try
                {
                    // keep method call order consistent
                    var userRepository = services.GetRequiredService<IUserRepository>();
                    var userRoleRepository = services.GetRequiredService<IUserRolesRepository>();
                    var roleRepository = services.GetRequiredService<IRolesRepository>();
                    var menuRepository = services.GetRequiredService<IMenuRepository>();
                    var roleMenuPermission = services.GetRequiredService<IRoleMenuPermissionRepository>();

                    await DataSeeder.SeedRolesAsync(roleRepository);
                    await DataSeeder.SeedUsersAsync(userRepository, roleRepository, userRoleRepository);
                    await DataSeeder.SeedMenuAsync(menuRepository);


                    //var dbContent = scope.ServiceProvider.GetRequiredService<AppIdentityDbContext>();
                    //dbContent.Database.Migrate();

                }
                catch (Exception ex)
                {
                    var logger = loggerFactory.CreateLogger<Program>();
                    logger.LogError(ex, "An Error During Seeding of Data");

                }


            }
            // Run Host
          await  host.RunAsync();
        }
        public static IHostBuilder CreateHostBuilder(string[] arg) =>

            Host.CreateDefaultBuilder(arg)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<StartUp>();
                });
    }

}
