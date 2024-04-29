using HRMS.Controllers;
using HRMS.Data.Dtos.Users;
using HRMS.Services.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Areas.Admin
{
    public class UserAuthController : BaseApiController
    {
        private readonly IUserAuthService _userAuthService;

        public UserAuthController(IUserAuthService userAuthService)
        {
            _userAuthService = userAuthService; 
        }
        [HttpPost("token")]
        [AllowAnonymous]
       public async Task<IActionResult> Token([FromBody] UserAuthRequestDto token)
       {
            var (status,response) = await _userAuthService.TokenAsync(token);
            return GetResponseFromStatusCode(status, response);

       }
    }
}
