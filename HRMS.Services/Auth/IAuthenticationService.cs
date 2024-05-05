using HRMS.Data.Dtos.Identity;
using HRMS.Data.Dtos.Response;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace HRMS.Services.Auth
{
    public interface IAuthenticationService
    {
        public Task<(HttpStatusCode, ApiResponseDto)> Login(loginDto loginDto);

        public Task<(HttpStatusCode, ApiResponseDto)> UserLogin(UserLoginDto userLoginDto, HttpRequest httpRequest, HttpContext httpContent);
        

    }
}
