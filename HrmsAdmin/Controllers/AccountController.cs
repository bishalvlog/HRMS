using HrmsAdmin.Common.Alerts.Types;
using HrmsSystemAdmin.Web.Commond.Auth;
using HrmsSystemAdmin.Web.Dto.Users.Auth;
using HrmsSystemAdmin.Web.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HrmsAdmin.Controllers
{
    
    public class AccountController : BaseController
    {
        private readonly IAuthService _authService;
        public AccountController(IAuthService authService)
        {
            _authService = authService;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("login")]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("index", "Home");
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public async Task <IActionResult> Login(UserLoginRequest loginRequest)
        {
            try
            {
                var token = await _authService.LoginWithPassword(loginRequest);
                if (token.Success)
                {
                    var userClaims = _authService.GetClaimsFromAccessToken(token.AccessToken);
                    userClaims.Add(new Claim(UserClaimTypes.AccessToken, token.AccessToken));
                    userClaims.Add(new Claim(UserClaimTypes.RefreshToken, token.RefreshToken));

                    var userIdentity = new ClaimsIdentity(userClaims,CookieAuthenticationDefaults.AuthenticationScheme);
                    var claimPrinciple = new ClaimsPrincipal(userIdentity);
                    var authProperties = new AuthenticationProperties();
                    authProperties.IsPersistent = loginRequest.rememberme;
                    authProperties.IssuedUtc = DateTime.UtcNow;


                    //signout
                    if (_authService.IsUserAuthenticated())
                        await _authService.LogoutAsync();

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimPrinciple, authProperties);
                }
                else
                {
                    TempData["invalid"] = token.Errors.Count > 0 ? token.Errors[0].ToString() : "Invalid Credentials!";
                }
                return RedirectToAction("index","Home");
            }
            catch (Exception)
            {
                Nofity("Login Failed!", "Sorry !", noticationType: NoticationType.Error);
                throw;
            }

        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
