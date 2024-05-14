using HrmsSystemAdmin.Web.Commond.Auth;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.CookiePolicy;

namespace HrmsSystemAdmin.Web.Extensions
{
    public static class AuthenticationServiceExtenstions
    {
        public static IServiceCollection AddAuthenticationService (this IServiceCollection services, IConfiguration config)
        {

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
                {
                    options.Cookie.Name = AuthDefaults.CookieAuthenticationName;
                    options.LoginPath = AuthDefaults.LoginPath;
                    options.LogoutPath = AuthDefaults.LogoutPath;
                    options.AccessDeniedPath = AuthDefaults.AccessDeniedPath;
                    options.SlidingExpiration = true;
                });
            services.AddAuthorization(config =>
            {
                config.DefaultPolicy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .AddAuthenticationSchemes(CookieAuthenticationDefaults.AuthenticationScheme)
                .Build();

                config.AddPolicy("MerchantPolicy", policy => policy
               .RequireAuthenticatedUser()
               .RequireRole("Merchant")
               .AddAuthenticationSchemes(CookieAuthenticationDefaults.AuthenticationScheme));
            });

            services.Configure<CookiePolicyOptions>(options =>
            {
                // prevent access from javascript 
                options.HttpOnly = HttpOnlyPolicy.Always;

                // If the URI that provides the cookie is HTTPS, 
                // cookie will be sent ONLY for HTTPS requests 
                // (refer mozilla docs for details) 
                options.Secure = CookieSecurePolicy.SameAsRequest;

                // refer "SameSite cookies" on mozilla website 
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromDays(2);
            });

            return services;

        }
    }
}
