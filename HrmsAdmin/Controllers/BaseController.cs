using HrmsAdmin.Common.Alerts.Types;
using HrmsSystemAdmin.Web.Commond.Auth;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HrmsAdmin.Controllers
{
    [Route("[controller]")]
    public class BaseController : Controller
    {

        public void Nofity(string message, string title, NoticationType  noticationType = NoticationType.Success)
        {
            var msg = new
            {
                Message = message,
                Title = title,
                Icon = noticationType.ToString(),
                Type = noticationType.ToString(),
                Provider = GetProvider()
            };
            TempData["Message"] = JsonConvert.SerializeObject(msg);
        }
        private string GetProvider()
        {
            var config = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                            .AddEnvironmentVariables()
                            .Build();

            return config["NotificationProvider"];
        }
        protected string GetLoggedInUserUserName()
        {
            return User.FindFirst(UserClaimTypes.UserName)?.Value;
        }

        protected string GetLoggedInUserEmail()
        {
            return User.FindFirst(UserClaimTypes.Email)?.Value;
        }

        protected string GetLoggedInUserIsSuperAdmin()
        {
            return User.FindFirst(UserClaimTypes.IsSuperAdmin)?.Value;
        }

        protected string GetLoggedInUserRole()
        {
            return User.FindFirst(UserClaimTypes.Role)?.Value;
        }

        protected string GetLoggedInUserExpireTime()
        {
            return User.FindFirst(UserClaimTypes.Exp)?.Value;
        }
      
    }
}
