using HRMS.Controllers;
using HRMS.Data.Dtos.Users;
using HRMS.Features.Auth.policies;
using HRMS.Services.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Areas.Admin
{
    [Authorize(UserPolicies.HrmsUser)]
    public class UserAuthController : BaseApiController
    {
        private readonly IUserAuthService _userAuthService;

        public UserAuthController(IUserAuthService userAuthService)
        {
            _userAuthService = userAuthService; 
        }
        [AllowAnonymous]
        [HttpPost("token")]
       public async Task<IActionResult> Token([FromBody] UserAuthRequestDto token)
       {
            var (status,response) = await _userAuthService.TokenAsync(token);
            return GetResponseFromStatusCode(status, response);

       }
    }
}
