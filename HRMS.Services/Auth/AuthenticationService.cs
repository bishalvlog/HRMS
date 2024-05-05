using HRMS.Core.Interfaces.Menu;
using HRMS.Core.Models.Identity;
using HRMS.Data.Comman.Constrant;
using HRMS.Data.Comman.Helpers;
using HRMS.Data.Data;
using HRMS.Data.Data.Entities;
using HRMS.Data.Dtos.Identity;
using HRMS.Data.Dtos.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace HRMS.Services.Auth
{
    public class AuthenticationService : IAuthenticationService
    {
        string _ClientDeviceIP = null;
        string _RemoteServerIP = null;

        private readonly HrmsContext _db;
        private readonly UserManager<AppDbUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IRoleMenuPermissionRepository _roleMenuPermissionRepository;
        private readonly IConfiguration _configure;

        public AuthenticationService(HrmsContext db, UserManager<AppDbUser> userManager, RoleManager<IdentityRole> roleManager, IRoleMenuPermissionRepository roleMenuPermissionRepository, IConfiguration configuration) 
        { 
            _db = db;
                _userManager = userManager;
            _roleManager = roleManager;
            _configure = configuration;
             _roleMenuPermissionRepository = roleMenuPermissionRepository;
            _configure = configuration;
        }
        public async Task<(HttpStatusCode, ApiResponseDto)> Login(loginDto loginDto)
        {
            var user = await _userManager.FindByNameAsync(loginDto.UserName);
            if (user != null && user.IsActive == true)
            {
                if((user.IsCustomer == null ? false : user.IsCustomer)== true) 
                return (HttpStatusCode.Unauthorized,new ApiResponseDto { Success = false, Message = AppString.Unauthorized, Errors = new List<string> { "Unauthorized !" } });

                if(await _userManager.CheckPasswordAsync(user,loginDto.Password))
                {
                    var userRole = await _userManager.GetRolesAsync(user);

                    var menu = await _roleMenuPermissionRepository.GetListWithSubMenusAsync(loginDto.UserName);

                    var authClaim = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.Id),
                        new Claim(ClaimTypes.Name, user.UserName),
                        new Claim(ClaimTypes.Email, user.Email ?? ""),
                        new Claim("MobileNumber",user.MobileNumber ?? ""),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),


                    };
                    if(userRole.Count > 0)
                    {
                        foreach(var role in userRole)
                        {
                            authClaim.Add(new Claim(ClaimTypes.Role, role));

                        }
                    }
                    var token = CreateToken(authClaim);

                    return (HttpStatusCode.OK, new ApiResponseDto
                    {

                        Success = true,
                        Data = new
                        {
                            token = new  JwtSecurityTokenHandler().WriteToken(token),
                            expiration = token.ValidTo,
                            userName = user.UserName,
                            id = user.Id,
                            menu
                            
                        }
                    });
                }

            }
            return (HttpStatusCode.Unauthorized, new ApiResponseDto { Success = false, Message = AppString.Unauthorized, Errors = new List<string> { "Unauthorizes" } });
           
        }
       
        public async Task<(HttpStatusCode, ApiResponseDto)> UserLogin(UserLoginDto userLoginDto, HttpRequest httpRequest, HttpContext httpContent)
        {
            var user = new AppDbUser();

            var userName = userLoginDto.UserName;

            if(userName.IndexOf('@')> -1)
            {
                user =  await _userManager.FindByNameAsync(userLoginDto.UserName);
                if (user == null)
                    return (HttpStatusCode.Unauthorized, new ApiResponseDto { Success = false, Message = AppString.Unauthorized, Errors = new List<string> { "UnAthorize" } });
                else
                    userName = user.Email;
            }
            else
            {
                user = await _userManager.FindByEmailAsync(userLoginDto
                    .UserName);
                if (user == null)
                    return (HttpStatusCode.Unauthorized, new ApiResponseDto { Success = false, Message = AppString.Unauthorized, Errors = new List<string> { "UnAthorize" } });
                else
                    userName = user.UserName;
            }

            if(user != null && user.IsActive == true)
            {
                if ((user.IsCustomer == null ? false : user.IsCustomer) == false || user.EmailConfirmed == false)
                    return (HttpStatusCode.Unauthorized, new ApiResponseDto { Success = false, Message = AppString.Unauthorized, Errors = new List<string> { "UnAthorixed" } });

                if(await _userManager.CheckPasswordAsync(user, userLoginDto.Password))
                {
                    var userRole = await _userManager.GetRolesAsync(user);

                    var menus = await _roleMenuPermissionRepository.GetListWithSubMenusAsync(userLoginDto.UserName);

                    var AuthClaim = new List<Claim>()
                    {
                        new Claim(ClaimTypes.NameIdentifier,user.Id),
                        new Claim(ClaimTypes.Name , user.UserName),
                        new Claim(ClaimTypes.Email, user.Email ?? ""),
                        new Claim(ClaimTypes.MobilePhone, user.MobileNumber ?? ""),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())

                    };
                    if(userRole != null)
                    {
                        if(userRole.Count >1)
                        {
                            foreach (var role in userRole)
                            {
                                AuthClaim.Add(new Claim(ClaimTypes.Role, role));    
                            }
                        }
                    }
                    string host = Dns.GetHostName();
                    _RemoteServerIP = Dns.GetHostEntry(host).AddressList[1].ToString();
                    _ClientDeviceIP = GetIpAddress(httpRequest, httpContent);
                    var loginIpDetails =new  TblUserLogingDetail();
                    if(_db.TblUserLogingDetails.Any(u => u.UserName.ToLower()== userLoginDto.UserName && u.ClientId == _configure["Hrms_ClientId"])) 
                    {
                        loginIpDetails = _db.TblUserLogingDetails.FirstOrDefault(u => u.UserName.ToLower()== userLoginDto.UserName && u.ClientId == _configure["Hrms_ClientId"]);
                        if(loginIpDetails != null)
                        {
                            loginIpDetails.ClientDeviceIp = _ClientDeviceIP;
                            loginIpDetails.RemoteServerIp = _RemoteServerIP;
                            loginIpDetails.LastLoginLocalTime = DateTime.Now;
                            loginIpDetails.LastLoginUtcTime = DateTime.UtcNow;
                            loginIpDetails.LastLoginNepaliDate = DateConversions.ConvertToNepaliDate(DateTime.Now);
                            await _db.SaveChangesAsync();
                                
                        }
                    }
                    else
                    {
                        loginIpDetails.UserName = userLoginDto.UserName;
                        loginIpDetails.ClientId = _configure["Hrms_ClientId"];
                        loginIpDetails.RemoteServerIp = _RemoteServerIP;
                        loginIpDetails.ClientDeviceIp = _ClientDeviceIP;
                        loginIpDetails.LastLoginLocalTime = DateTime.Now;
                        loginIpDetails.LastLoginUtcTime = DateTime.UtcNow;
                        loginIpDetails.LastLoginNepaliDate = DateConversions.ConvertToNepaliDate(DateTime.Now);
                        await _db.TblUserLogingDetails.AddAsync(loginIpDetails);
                        await _db.SaveChangesAsync();

                    }
                    var token = CreateToken(AuthClaim);
                    return (HttpStatusCode.OK, new ApiResponseDto
                    {
                        Success = true,
                        Data = new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(token),
                            expiration = token.ValidTo,
                            username = userLoginDto.UserName,
                            id = user.Id

                        }
                    });


                }

            }
            return (HttpStatusCode.Unauthorized, new ApiResponseDto { Success = false, Message = AppString.Unauthorized, Errors = new List<string> { "Unathorized !" } });
        }
        private JwtSecurityToken CreateToken(List<Claim> authClaim)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configure["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configure["JWT:ValidIssues"],
                audience: _configure["JWT:ValidAudience"],
                expires: DateTime.Now.AddDays(1),
                claims: authClaim,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            return token;
        }

        private string GetIpAddress(HttpRequest httpRequest, HttpContext httpContent)
        {
            if (httpRequest.Headers.ContainsKey("X-Forwarded-For"))
            return httpRequest.Headers["X-Forwarded-For"];

            else
            {
               return httpContent.Connection.RemoteIpAddress.MapToIPv4().ToString();
            }
        }

    }
}
