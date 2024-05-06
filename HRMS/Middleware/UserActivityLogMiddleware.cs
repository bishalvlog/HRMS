using HRMS.Extensions;
using HRMS.Features.Auth.Schema.CustomerJwt;
using HRMS.Features.Auth.Schema.UserJwt;
using HRMS.Mvc.Filters;
using HRMS.Services.Commond.Helpers;
using HRMS.Services.Logger;
using HRMS.Services.Token;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Logging;
using System.Security.Claims;

namespace HRMS.Middleware
{
    public class UserActivityLogMiddleware
    {
        private readonly RequestDelegate _requestDelegate;

        public UserActivityLogMiddleware(RequestDelegate requestDelegate)
        {
            _requestDelegate = requestDelegate;
        }
        public async Task InvokeAsync(HttpContext httpContext, IUserActivityLogService userActivityLogService)
        {
            if (httpContext.Request.Path.StartsWithSegments("/swagger"))
            {
                await _requestDelegate(httpContext);
                return;
            }
            //for Customers

            var authResults = await httpContext.AuthenticateAsync(CustomerJwtBearerAuthenticationOptions.DefaultSchema);
            if (authResults.Succeeded)
            {
                await LogUserActivityAsync(httpContext, authResults.Principal, userActivityLogService);
                await _requestDelegate(httpContext);
                return;
            }
            //for admon 
            authResults = await httpContext.AuthenticateAsync(UserJwtBearerAuthenticationOptions.DefaultSchema);
            if (authResults.Succeeded)
            {
                await LogUserActivityAsync(httpContext,authResults.Principal, userActivityLogService);
                await _requestDelegate(httpContext);
                return;
                
            }
            await _requestDelegate(httpContext);
            return;
        }
        private async Task  LogUserActivityAsync(HttpContext context, ClaimsPrincipal user,IUserActivityLogService userActivityLogService)
        {
            try
            {
                var userActivitylogParam = await LogHelpers.GetUserActivityLogAsync(context);
                userActivitylogParam.UserName = GetUserName(user);
                userActivitylogParam.Email = GetEmail(user);

                await userActivityLogService.LogAsync(userActivitylogParam);
               
            }catch(Exception) { }

        }
        private string GetUserName (ClaimsPrincipal user)
        {
            return user?.FindFirst(c => c.Type == CustomerClaimTypes.CustomerId || c.Type == UserClaimTypes.UserName)?.Value ?? user?.Identity?.Name;

        }
        private string GetEmail(ClaimsPrincipal user)
        {
            return user?.FindFirst(c => c.Type == CustomerClaimTypes.Email || c.Type == UserClaimTypes.Email)?.Value;
        }
    }
}
