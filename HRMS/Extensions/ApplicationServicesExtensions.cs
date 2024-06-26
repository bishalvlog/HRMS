﻿using DigitalGold.Core.Configuration;
using DigitalGold.Services.Tokens.RefreshTokens;
using HRMS.Core.Interfaces.Logging;
using HRMS.Core.Interfaces.Menu;
using HRMS.Core.Interfaces.Users;
using HRMS.Data.Comman.Constrant;
using HRMS.Data.Dtos.Response;
using HRMS.Data.Logging;
using HRMS.Data.Repository.ClientMenu;
using HRMS.Data.Repository.Logging;
using HRMS.Data.Repository.Menu;
using HRMS.Data.Repository.Users;
using HRMS.Services.Auth;
using HRMS.Services.ClientMenu;
using HRMS.Services.Logger;
using HRMS.Services.Menu;
using HRMS.Services.SecureApi;
using HRMS.Services.Token.jwt;
using HRMS.Services.Users;
using Microsoft.AspNetCore.Identity;
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
            #region logger
            services.AddScoped<IExceptionLogRepository,ExceptionalLogRepository>();
            services.AddScoped<IUserActivityLogRepository, UserActivityLogRepository>();
            services.AddScoped<IExceptionLogService, ExceptionLogService>();
            
            #endregion

            #region AuthServices

            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IExceptionLogService, ExceptionLogService>();
            services.AddScoped<IClientMenuService, ClientMenuService>();
            services.AddScoped<IClientMenuRepository, ClientMenuRepository>();
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
            services.Configure<RouteOptions>(options => options.LowercaseUrls = true);

            // ApiBehavior configuration for invalid API model state
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    var errors = actionContext.ModelState
                        .Where(e => e.Value.Errors.Count > 0)
                        .SelectMany(x => x.Value.Errors)
                        .Select(x => x.ErrorMessage)
                        .ToList();

                    var errorResponse = new ApiResponseDto
                    {
                        Success = false,
                        Message = AppString.BadRequest,
                        Errors = errors
                    };

                    return new BadRequestObjectResult(errorResponse);
                };
            });

            services.Configure<DataProtectionTokenProviderOptions>(options =>
            {
                ////reset token valid for 2 hours
                options.TokenLifespan = TimeSpan.FromHours(2);
            });

            // Options pattern for Jwt configurations

            services.Configure<UserJwtOptions>(cong.GetSection(UserJwtOptions.JwtOptions));
            services.Configure<CustomerJwtOptions>(cong.GetSection(CustomerJwtOptions.JwtOption));
            services.Configure<CustomerRefreshTokenOptions>(cong.GetSection(CustomerRefreshTokenOptions.SectionName));


            
            services.AddOptions<AutoRateUpdateTime>().BindConfiguration(AutoRateUpdateTime.SectionName).ValidateDataAnnotations();

            return services;
        }
    }
}
