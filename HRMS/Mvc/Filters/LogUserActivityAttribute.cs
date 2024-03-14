using HRMS.Services.Commond.Helpers;
using HRMS.Services.Logger;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Logging;

namespace HRMS.Mvc.Filters
{
    public sealed class LogUserActivityAttribute : TypeFilterAttribute
    {
        public LogUserActivityAttribute(string userAction = "" , bool LogRequestBody = true) : base(typeof(LogUserActivityFilter)) 
        {
            Arguments = new object[] { userAction, LogRequestBody };

        }
        #region Nested Filter 
        public class LogUserActivityFilter : IAsyncResourceFilter
        {
            private readonly string _userAction;
            private readonly bool _LogRequestBody;
            private readonly IUserActivityLogService _userActivityLogService;
            private IHostEnvironment _hostEnv;
            public LogUserActivityFilter(string userAction, bool logRequestBody, IUserActivityLogService userActivityLogService, IHostEnvironment hostEnv)
            {
                _userAction = userAction;
                _LogRequestBody = logRequestBody;
                _userActivityLogService = userActivityLogService;
                _hostEnv = hostEnv;
            }

            public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
            {
                await LogUserActivityAsync(context.HttpContext);
                await next();
            }
            private async Task LogUserActivityAsync(HttpContext context)
            {
                try
                {
                    var userActivityLogParam = await LogHelpers.GetUserActivityLogAsync(context, _hostEnv,logRequestBody:_LogRequestBody,UserAction : _userAction);
                    await _userActivityLogService.LogAsync(userActivityLogParam);

                }
                catch (Exception ex)
                {

                }
            }
        }
        #endregion 

    }
}
