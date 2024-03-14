using HRMS.Data.Dtos.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Services.Users
{
    public interface IUserRoleServices
    {
        Task<(HttpStatusCode, ApiResponseDto)> AssignUserToRolesAsync(int userId,params int[] roleIds);
    }
}
