using HRMS.Core.Models.Identity;
using HRMS.Core.Models.Users;
using HRMS.Data.Identity;
using Microsoft.AspNetCore.Identity;

namespace HRMS.Extensions
{
    public static class AddIdentityServices
    {
        public static IServiceCollection AddIdentityService (this IServiceCollection services, IConfiguration cong)
        {
            services.AddIdentity<AppDbUser, IdentityRole>(option =>
            {
                option.Password.RequireDigit = true;
                option.Password.RequireUppercase = true;
                option.Password.RequireLowercase = true;
                option.Password.RequireNonAlphanumeric = true;
                option.Password.RequiredLength = 8;

                option.User.RequireUniqueEmail = true;

            })
                .AddEntityFrameworkStores<AppIdentityDbContext>()
                .AddDefaultTokenProviders();
            return services;

        }
    }
}
