using HrmsSystemAdmin.Web.Dto.Users.Auth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth;

namespace HrmsSystemAdmin.Web.Data.Repository
{
    public interface IAuthRepository
    {
        Task<AuthTokenResponse> Login(UserLoginRequest userlogin); 
    }

}
