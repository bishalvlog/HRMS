using HrmsSystemAdmin.Web.Dto.Users.Auth;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace HrmsSystemAdmin.Web.Services
{
    public interface IAuthService
    {
        Task<AuthTokenResponse> LoginWithPassword(UserLoginRequest request);
        Task<AuthTokenResponse> LoginWithRefressToken(UserLoginRequest loginRequestDto);
        bool IsUserAuthenticated();
        Task LogoutAsync();
        DateTimeOffset GetAccessTokenExpirationDateTime(string token);
        void RemoveJWTClaimsFromAccessToken(string accessToken);
        List<Claim> GetClaimsFromAccessToken(string accessToken);

    }
}
