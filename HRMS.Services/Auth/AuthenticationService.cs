using Azure.Identity;
using HRMS.Core.Interfaces.Menu;
using HRMS.Core.Models.Identity;
using HRMS.Core.Models.Menu;
using HRMS.Data.Comman.Constrant;
using HRMS.Data.Data;
using HRMS.Data.Dtos.Identity;
using HRMS.Data.Dtos.Response;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

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
        private JwtSecurityToken CreateToken (List<Claim> authClaim)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configure["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configure["JWT:ValidIssues"],
                audience: _configure["JWT:ValidAudience"],
                expires : DateTime.Now.AddDays(1),
                claims :  authClaim,
                signingCredentials : new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            return token;
        }
    }
}
