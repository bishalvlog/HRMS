using HrmsSystemAdmin.Web.Data.Repository;
using HrmsSystemAdmin.Web.Services;
using HrmsSystemAdmin.Web.Services.http;
using HrmsSystemAdmin.Web.Services.http.HrmsApi;

namespace HrmsSystemAdmin.Web.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicatonServices(this IServiceCollection services)
        {
            services.AddScoped<IHrmsClient, HrmsClient>();

            #region Repository
            services.AddScoped<IAuthRepository, AuthRepository>();
            #endregion

            #region service
            services.AddScoped<IAuthService, AuthServices>();
            #endregion
            return services;
        }
        public static IServiceCollection AddHostedApplicationServices(this IServiceCollection services)
        {
            return services;
        }

        public static IServiceCollection ConfigureApplicationAndServices(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<RouteOptions>(options => options.LowercaseUrls = true);

            return services;
        }
    }
}
