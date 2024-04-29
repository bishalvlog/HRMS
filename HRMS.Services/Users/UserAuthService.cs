using Azure.Core;
using HRMS.Core.Domain;
using HRMS.Core.Interfaces.Token;
using HRMS.Core.Models.Auth;
using HRMS.Core.Models.Users;
using HRMS.Data.Comman.Constrant;
using HRMS.Data.Comman.Helpers;
using HRMS.Data.Dtos.Auth;
using HRMS.Data.Dtos.Response;
using HRMS.Data.Dtos.Users;
using HRMS.Services.Commond.Helpers;
using HRMS.Services.Logger;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Logging;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Services.Users
{
    public class UserAuthService : IUserAuthService
    {
        private readonly IHostEnvironment _hostEnv;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserAccountService _userAccountService;
        private readonly IUserActivityLogService _logServices;
        private readonly IUserRoleServices _userUserRoleService;
        private readonly TokenService _tokenService;
        private readonly IRefreshTokenService _refreshTokenService;

        public UserAuthService(IUserAccountService userAccountService)
        {
            _userAccountService = userAccountService;
        }
        
        public async  Task<(HttpStatusCode, ApiResponseDto)> TokenAsync(UserAuthRequestDto userAuth)
        {
           switch(userAuth.GrantType)
            {
                case AuthGrantType.Password:
                    {
                        var user = CommonHelper.IsValidEmail(userAuth.UserNameOrEmail)
                            ? await _userAccountService.GetByEmailAsync(userAuth.UserNameOrEmail) :
                            await _userAccountService.GetByUserNameAsync(userAuth.UserNameOrEmail);
                        if (user is null)
                            return (HttpStatusCode.Unauthorized,
                                    new UserAuthResponseDto { Success = false, Message = AppString.Unauthorized, Errors = new() { "Invalid Log in Credentials !" } });
                        if (!await _userAccountService.CheckPasswordAsync(user, userAuth.Passord))
                            return (HttpStatusCode.Unauthorized, new UserAuthResponseDto { Success = false, Message = AppString.Unauthorized, Errors = new() { "Invalid Login Credentials" } });
                        if (!user.IsActive)
                            return (HttpStatusCode.Forbidden, new UserAuthResponseDto { Success = false, Message = AppString.Forbidden, Errors = new() { "Account not acctive, Contact Your Administrator" } });
                        user.LastLoginDate = DateTime.Now;

                        var resUpdate = await _userAccountService.UpdateAsync(user);
                        if(!resUpdate.Success)
                            return (HttpStatusCode.InternalServerError, new UserAuthResponseDto { Success =  false, Message = AppString.InternalServerError, Errors = new() { AppString.InternalServerError } });
                        var userRole = await _userUserRoleService.GetUserRolesByNames(user.UserName);
                        var (accesstoken, refreshtoken) = _tokenService.CreateUserJwtTokenWithRefreshToken(user, userRole);

                        var status = await _refreshTokenService.CreateAsync(refreshtoken);

                        if (status != HttpStatusCode.OK)
                            return (HttpStatusCode.InternalServerError, 
                                new UserAuthResponseDto { Success = false, Message = AppString.InternalServerError, Errors = new() { AppString.InternalServerError } });
                        var response = GenerateAuthResponseWithToken(accesstoken,refreshtoken);

                        await LogUserLoginActivetyAsync(user);

                        return (HttpStatusCode.OK, response);

                    }
                case AuthGrantType.RefreshToken:
                    {
                        var storeRefreshToken = await _refreshTokenService.GetAsync(userAuth.RefreshToken);
                        if (storeRefreshToken is null)
                            return (HttpStatusCode.Unauthorized, 
                                new UserAuthResponseDto { Success = false, Message = AppString.Unauthorized, Errors = new() { "Sessin Expired !" } });

                        if (string.IsNullOrWhiteSpace(storeRefreshToken.UserName))
                            return (HttpStatusCode.Unauthorized, new UserAuthResponseDto { Success = false, Message = AppString.Unauthorized, Errors = new() { "Session Expired !" } });

                        if (storeRefreshToken.IsRevoked || storeRefreshToken.ExpiredUtcTime < DateTime.UtcNow)
                            return (HttpStatusCode.Unauthorized, new UserAuthResponseDto { Success = false, Message = AppString.Unauthorized, Errors = new() { "Session Expired !" } });

                        var user = await _userAccountService.GetByUserNameAsync(storeRefreshToken.UserName);
                        if (!user.IsActive)
                            return (HttpStatusCode.Forbidden,
                                new UserAuthResponseDto { Success = false, Message = AppString.Forbidden, Errors = new() { "Account Not Active, Contact Your Administrator" } });
                        user.LastLoginDate = DateTime.UtcNow;

                        var resUpdate = await _userAccountService.UpdateAsync(user);
                        if (!resUpdate.Success)
                            return (HttpStatusCode.InternalServerError,
                                new UserAuthResponseDto { Success = false, Message = AppString.InternalServerError, Errors = new() { "Internal Server Error" } });

                        var userRole = await _userUserRoleService.GetUserRolesByNames(user.UserName);
                        var (accessToken, refreshToken) = _tokenService.CreateUserJwtTokenWithRefreshToken(user, userRole, storeRefreshToken);

                        var status = await _refreshTokenService.UpdateAsync(refreshToken);
                        if (status != HttpStatusCode.OK) return (HttpStatusCode.InternalServerError, new UserAuthResponseDto { Success = false, Message = AppString.InternalServerError, Errors = new() { "Internal Server Error" } });

                        var response = GenerateAuthResponseWithToken(accessToken, refreshToken);
                        return (HttpStatusCode.OK, response);   

                    }
                default:
                    return (HttpStatusCode.BadRequest, new UserAuthResponseDto { Success = false, Message = AppString.BadRequest, Errors = new() { "Invalid Reques" } });
                    
            }
        }
        private UserAuthResponseDto GenerateAuthResponseWithToken(string token, RefreshToken refreshToken)
        {
            return new UserAuthResponseDto
            {
                UserName = refreshToken.UserName,
                Success = true,
                AccessToken = token,
                RefreshToken = refreshToken.Token,
                TokenType = AuthTokenType.Bearer,
                ExpiresIn = DateTimeOffset.Now.AddMinutes(UserConstant.AccessTokenExpireMminutes).AddSeconds(-1).ToUnixTimeMilliseconds(),

            };
        }
        private async Task LogUserLoginActivetyAsync(AppUser users)
        {
            try
            {
                var userActivityLogParam = await LogHelpers.GetUserActivityLogAsync(_httpContextAccessor.HttpContext, _hostEnv, logRequestBody: false, UserAction: UserActionTypes.ActionUserLoggedIn);
                userActivityLogParam.UserName = users.UserName;
                userActivityLogParam.Email = users.Email;
                userActivityLogParam.RequestBody = null;

                await _logServices.LogAsync(userActivityLogParam);
            }
            catch(Exception ex)
            {

            }
        }
    }
}
