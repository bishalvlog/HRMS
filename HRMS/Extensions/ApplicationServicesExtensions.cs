using HRMS.Core.Interfaces.Menu;
using HRMS.Core.Interfaces.Users;
using HRMS.Data.Comman.Constrant;
using HRMS.Data.Dtos.Response;
using HRMS.Data.Repository.Menu;
using HRMS.Data.Repository.Users;
using HRMS.Services.Auth;
using HRMS.Services.Logger;
using HRMS.Services.Menu;
using HRMS.Services.SecureApi;
using HRMS.Services.Users;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddConfig(this IServiceCollection services)
        {
            #region  User
            services.AddScoped<IRolesRepository, RolesRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserRolesRepository, UserRolesRepository>();


            services.AddScoped<IRoleServices, RolesServices>();
            services.AddScoped<IUserServices, UserServices>();
            services.AddScoped<IUserRoleServices, UserRoleServices>();
            services.AddScoped<IUserAccountService, UserAccountService>();
            services.AddScoped<IUserAuthService,  UserAuthService>();
            #endregion

            #region AuthServices

            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IExceptionLogService, ExceptionLogService>();
            #endregion
            #region Menus
            services.AddScoped<IRoleMenuPermissionRepository, RoleMenuPermissionRepository>();
            services.AddScoped<IMenuRepository, MenusRepository>();

            services.AddScoped<IRoleMenuPermissionService, RoleMenuPermissionService>();
            services.AddScoped<IMenusService, MenusService>();
                
            #endregion
            #region Crypto
            services.AddScoped<ISecureApiCrytoService, SecureApiCrytoService>();
            #endregion
            return services;
        }
        public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services, IConfiguration cong)
        {
            services.Configure<RouteOptions>(option => option.LowercaseUrls = true);

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    var error = actionContext.ModelState
                    .Where(e => e.Value.Errors.Count > 0)
                    .SelectMany(x => x.Value.Errors)
                    .Select(x => x.ErrorMessage)
                    .ToList();

                    var errorResponse = new ApiResponseDto
                    {
                        Success = false,
                        Message = AppString.BadRequest,
                        Errors = error

                    };
                    return new BadRequestObjectResult(errorResponse);

                };
            });
            return services;

        }
    }
}
