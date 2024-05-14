using HrmsSystemAdmin.Web.Data.Repository;
using HrmsSystemAdmin.Web.Dto.Users.Auth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace HrmsSystemAdmin.Web.Services.http
{
    public class AuthServices : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly HttpContext _httpContent;
        public AuthServices(IAuthRepository authRepository, IHttpContextAccessor httpContextAccessor)
        {
            _authRepository = authRepository;
            _httpContent = httpContextAccessor.HttpContext;
        }

        public DateTimeOffset GetAccessTokenExpirationDateTime(string token)
        {

            if (token is null)
                throw new ArgumentNullException(nameof(token));
            var expDateTime = new JwtSecurityTokenHandler()
                .ReadJwtToken(token)
                .Claims
                .First(claim => claim.Type == JwtRegisteredClaimNames.Exp).Value;

            return DateTimeOffset.FromUnixTimeSeconds(long.Parse(expDateTime));

        }

        public List<Claim> GetClaimsFromAccessToken(string accessToken)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(accessToken);

            var claims = new List<Claim>();
            claims.AddRange(jwt.Claims);
            return claims;
        }

        public bool IsUserAuthenticated()
        {
            return _httpContent.User.Identity.AuthenticationType == CookieAuthenticationDefaults.AuthenticationScheme
                 && _httpContent.User.Identity.IsAuthenticated;
        }

        public async Task<AuthTokenResponse> LoginWithPassword(UserLoginRequest request)
        {
            request.GrantType = "password";
            request.Source = "web";
            var tokenData =  await _authRepository.Login(request);
            return tokenData;
        }

        public async Task<AuthTokenResponse> LoginWithRefressToken(UserLoginRequest loginRequestDto)
        {
            loginRequestDto.GrantType = "refresh_token";
            loginRequestDto.Source = "web";
            var tokenData = await _authRepository.Login(loginRequestDto);
            return tokenData;
        }

        public async Task LogoutAsync()
        {
            await SignOutUserAsync();
        }
        private async Task SignOutUserAsync()
        {
            await _httpContent.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        public void RemoveJWTClaimsFromAccessToken(string accessToken)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(accessToken);
            var claims = new List<Claim>();
            foreach (var claim in jwt.Claims)
            {
                claims.Remove(claim);
            }
        }
    }
}
