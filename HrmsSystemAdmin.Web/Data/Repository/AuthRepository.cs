using HrmsSystemAdmin.Web.Dto.Users.Auth;
using HrmsSystemAdmin.Web.Services.http.HrmsApi;
using Microsoft.AspNetCore.Authentication.OAuth;

namespace HrmsSystemAdmin.Web.Data.Repository
{
    public class AuthRepository :BaseRepository, IAuthRepository
    {
        private readonly IHrmsClient _hrmsClient;
        public AuthRepository (IHrmsClient hrmsClient)
        {
            _hrmsClient = hrmsClient;
        }
        public async Task<AuthTokenResponse> Login(UserLoginRequest userlogin)
        {
            var bodyContent = GetJsonStringContent(userlogin);
            var (statusCode, tokenData) = await _hrmsClient.PostAsync<AuthTokenResponse>(HrmsApiUrl.UserAuthRequestUri,bodyContent);
            return tokenData;
        }
    }
}
