using HRMS.Core.Models.Logger;
using HRMS.Services.Commond.Extension;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Services.Commond.Helpers
{
    public static class LogHelpers
    {
        public static async Task<UserActivitylogParam>GetUserActivityLogAsync(HttpContext context, IHostEnvironment hostEnv = null, bool logRequestBody = true, string UserAction = "")
        {
            var remoteIpAddress = context.GetIpAddress();
            var httpMethod = context.Request.Method;
            var requestUrl = context.GetRequestUrl();
            var queryString = context.GetQueryString();
            var userAgent = context.GetUserAgent();
            var headers = context.GetRequestHeaders();
            var controllerName = context.GetController();
            var actionName = context.GetAction();
            var environment = hostEnv?.EnvironmentName ?? null;

            var activitylogParam = new UserActivitylogParam
            {
                RequestUrl = requestUrl,
                QueryString = string.IsNullOrEmpty(queryString) ? null : queryString,
                Environment = environment,
                RemoteIpAddress = remoteIpAddress,
                HttpMethod = httpMethod,
                ActionName = actionName,
                ControllerName = controllerName,
                UserAgent = userAgent,
                Headers = headers,
                MachineName = Environment.MachineName,
                UserAction = actionName
            };
            if (logRequestBody)
            {
                var (isForm, requestBody) = await context.GetRequestBodyAsString();
                activitylogParam.IsFormData = isForm;
                activitylogParam.RequestBody = requestBody;
            }
            if (context.User.Identity.IsAuthenticated)
            {
                var userName = context.GetUserName();
                var email = context.GetUserEmail();
                var isCustomer = context.IsCustomer();

                activitylogParam.UserName = userName;
                activitylogParam.Email = email;
                activitylogParam.IsCustomer = isCustomer;
            }

            return activitylogParam;

        }
        public static async Task<AppExceptionLogParam> GetExceptionLogAsync (Exception ex, HttpContext context = null ,IHostEnvironment host = null)
        {
            var appExceptionLogParam = new AppExceptionLogParam
            {
                ExceptionMessage = ex.Message,
                ExceptionStackTrace = ex.StackTrace,
                ExceptionType = ex.GetType().FullName,
                InnerExceptionMessage = ex.InnerException?.Message,
                InnerExceptionStackTrace = ex.InnerException?.StackTrace,
                MachineName = Environment.MachineName,
                Enviroment = host.EnvironmentName ?? null
            };
            if(context is not null)
            {
                var remoteIpAddress = context.GetIpAddress();
                var requestUrl = context.GetRequestUrl();
                var queryString = context.GetQueryString();
                var httpMethod = context.Request.Method;
                var userAgent = context.GetUserAgent();
                var requestHeader = context.GetRequestHeaders   ();
                var userName = context.GetUserName();
                var (isForm, requestBody) = await context.GetRequestBodyString();

                appExceptionLogParam.RemoteIPAddress = remoteIpAddress;
                appExceptionLogParam.RequestUrl = requestUrl;
                appExceptionLogParam.QuerString = queryString;
                appExceptionLogParam.HttpMethod = httpMethod;
                appExceptionLogParam.UserAgent = userAgent;
                appExceptionLogParam.Headers = requestHeader;
                appExceptionLogParam.UserName = userName;
                if(isForm) appExceptionLogParam.FormDate = requestBody;
                else appExceptionLogParam.RequestBody = requestBody;
            }
            return appExceptionLogParam;


        }
    }
}
