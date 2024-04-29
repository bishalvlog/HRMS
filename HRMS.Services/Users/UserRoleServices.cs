using HRMS.Core.Interfaces.Users;
using HRMS.Core.Models.Users;
using HRMS.Data.Dtos.Response;
using HRMS.Services.Commond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Services.Users
{
    public class UserRoleServices : BaseService, IUserRoleServices
    {
        private readonly IUserRolesRepository _userRolesRepository;

        public UserRoleServices(IUserRolesRepository userRolesRepository)
        {
            _userRolesRepository = userRolesRepository;
        }
        public async Task<(HttpStatusCode, ApiResponseDto)> AssignUserToRolesAsync(int userId, params int[] roleIds)
        {
            if (!roleIds.Any())
                return (HttpStatusCode.BadRequest, 
                    new ApiResponseDto { Success = false, Message = "Bad Request !", Errors = new List<string> { "At Least one Roles id is Required !" }});
            var spMessage =  await _userRolesRepository.AssignUserToRolesAsync(userId,roleIds);
            if(spMessage.StatusCode !=200) return GetErrorResponseFromSprocMessage(spMessage);

            return (HttpStatusCode.OK, new ApiResponseDto { Success = true, Message =spMessage.MsgText });
        }

        public async Task<IEnumerable<AppRole>> GetUserRolesByNames(string UserName)
        {
            return await _userRolesRepository.GetUserRolesByNameAsync(UserName);
        }
    }
}
