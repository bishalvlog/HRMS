using HRMS.Features.Auth.Schema.CustomerJwt;
using HRMS.Features.Auth.Schema.UserJwt;
using HRMS.Services.Token.jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;

namespace HRMS.Extensions
{
    public static  class AuthServiceExtensions
    {
        public static IServiceCollection AddAuthService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication()
            .AddJwtBearer(CustomerJwtBearerAuthenticationOptions.DefaultSchema,options =>
            {
                var customerJwtOptions = configuration.GetSection(CustomerJwtOptions.JwtOption).Get<CustomerJwtOptions>();
                options.SaveToken = false;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                   ValidateIssuer = true,
                   ValidateAudience = true,
                   ValidateLifetime = true,
                   ValidateIssuerSigningKey = true,
                   ClockSkew = TimeSpan.Zero,
                   ValidAudience = customerJwtOptions.validAudience,
                   ValidIssuer = customerJwtOptions.validUser,
                   IssuerSigningKey = customerJwtOptions.IssueSignKey
                };
            })
                .AddJwtBearer(UserJwtBearerAuthenticationOptions.DefaultSchema, options =>
                {
                    var userJwtOptions = configuration.GetSection(UserJwtOptions.JwtOptions).Get<UserJwtOptions>();
                    options.SaveToken = false;
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ClockSkew = TimeSpan.Zero,
                        ValidAudience = userJwtOptions.validAudience,
                        ValidIssuer = userJwtOptions.validUser,
                        IssuerSigningKey = userJwtOptions.IssueSignKey
                    };
                });

            services.AddAuthorization(options =>
            {
                options.DefaultPolicy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .AddAuthenticationSchemes(
                        CustomerJwtBearerAuthenticationOptions.DefaultSchema,
                        UserJwtBearerAuthenticationOptions.DefaultSchema)
                    .Build();

                // Add application auth policies defined in Features/Auth/Policies directory
                //options.AddPolicy();


            });
            return services;
            

        }
    }
}
