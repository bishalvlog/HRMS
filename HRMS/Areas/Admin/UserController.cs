using HRMS.Controllers;
using HRMS.Core.Domain;
using HRMS.Core.Dtos.Users;
using HRMS.Core.Interfaces.Users;
using HRMS.Core.Models.Users;
using HRMS.Data.Dtos.UserDto;
using HRMS.Mvc.Filters;
using HRMS.Services.Users;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace HRMS.Areas.Admin
{
    public class UserController : BaseApiController
    {
        private readonly IUserServices _userServices;
        private readonly IUserRoleServices _userRoleServices;

        public UserController(IUserServices userServices, IUserRoleServices userRoleServices)
        {
            _userServices = userServices;
            _userRoleServices = userRoleServices;
        }
        [HttpPost, Route("Create-User")]
        [LogUserActivity(UserActionTypes.ActionCreatedUser)]
        public  async Task<IActionResult>CreateUser([FromForm] CreateUserDto appUser)
        {
            var (status, Response) = await _userServices.CreateUserAsync(appUser);
            return GetResponseFromStatusCode(status, Response);
        }
        [HttpGet("Get_User_By_Id")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetUserById([Required] int userId)
        {
            var (status, response) = await _userServices.GetUserByIdAsync(userId);
            return GetResponseFromStatusCode(status, response);
        }
        [HttpPost("Get_user_All")]
        public async Task<IActionResult> GetUserAll([FromBody] UserListRequest request)
        {
            var (status, response) = await _userServices.GetUserAsync(request);
            return GetResponseFromStatusCode(status, response); 
        }

    }
}
