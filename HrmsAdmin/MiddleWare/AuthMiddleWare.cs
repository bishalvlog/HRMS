using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using HrmsSystemAdmin.Web.Services;
using HrmsSystemAdmin.Web.Commond.Auth;
using HrmsSystemAdmin.Web.Dto.Users.Auth;
using HrmsSystemAdmin.Web.Services.http.HrmsApi;

namespace HrmsSystemAdmin.Web.MiddleWare
{
    public   class AuthMiddleWare
    {
        private readonly RequestDelegate _next;

        public AuthMiddleWare(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext httpContext, IAuthService authService)
        {

            if (authService.IsUserAuthenticated())
            {
                var accessToken = httpContext.User.FindFirst(UserClaimTypes.AccessToken)?.Value;

                if (string.IsNullOrWhiteSpace(accessToken))
                {
                    await authService.LogoutAsync();
                    await httpContext.ChallengeAsync(new AuthenticationProperties
                    {
                        RedirectUri = httpContext.Request.Path,
                    });
                    return;
                }

                var accessTokenExpirationDateTime = authService.GetAccessTokenExpirationDateTime(accessToken);
                if (accessTokenExpirationDateTime.AddSeconds(-5) < DateTime.UtcNow)
                {
                    var refreshToken = httpContext.User.FindFirst(UserClaimTypes.RefreshToken)?.Value;
                    if (string.IsNullOrWhiteSpace(refreshToken))
                    {
                        await authService.LogoutAsync();
                        await httpContext.ChallengeAsync(new AuthenticationProperties
                        {
                            RedirectUri = httpContext.Request.Path,
                        });
                        return;
                    }

                    var userLoginDto = new UserLoginRequest { RefreshToken = refreshToken };
                    var tokenData = await authService.LoginWithRefressToken(userLoginDto);

                    if (!tokenData.Success)
                    {
                        await authService.LogoutAsync();
                        await httpContext.ChallengeAsync(new AuthenticationProperties
                        {
                            RedirectUri = httpContext.Request.Path,
                        });
                        return;
                    }

                    await authService.LogoutAsync();


                    var userClaims = authService.GetClaimsFromAccessToken(tokenData.AccessToken);
                    userClaims.Add(new Claim(UserClaimTypes.AccessToken, tokenData.AccessToken));
                    userClaims.Add(new Claim(UserClaimTypes.RefreshToken, tokenData.RefreshToken));

                    var userIdentity = new ClaimsIdentity(userClaims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var claimsPrincipal = new ClaimsPrincipal(userIdentity);
                    var authProperties = new AuthenticationProperties();

                    authProperties.IssuedUtc = DateTime.UtcNow;


                    await httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal, authProperties);
                    if (!httpContext.Request.Headers.ContainsKey(HrmsApiDefaults.HeaderHrmsApiAccessToken))
                        httpContext.Request.Headers.Add(HrmsApiDefaults.HeaderHrmsApiAccessToken, tokenData.AccessToken);
                }

                if (!httpContext.Request.Headers.ContainsKey(HrmsApiDefaults.HeaderHrmsApiAccessToken))
                    httpContext.Request.Headers.Add(HrmsApiDefaults.HeaderHrmsApiAccessToken, accessToken);
            }

            await _next(httpContext);
        }
    }
    public static class AuthMiddlewareExtension
    {
        public static IApplicationBuilder UseAuthMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AuthMiddleWare>();
        }
    }


}
