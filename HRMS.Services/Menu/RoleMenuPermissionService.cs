using HRMS.Core.Interfaces.Menu;
using HRMS.Data.Comman.Constrant;
using HRMS.Data.Dtos.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Services.Menu
{
    public class RoleMenuPermissionService : IRoleMenuPermissionService
    {
        private readonly IRoleMenuPermissionRepository _roleMenuPermission;
        public RoleMenuPermissionService(IRoleMenuPermissionRepository roleMenuPermission)
        {
            _roleMenuPermission = roleMenuPermission;
        }
        public async Task<(HttpStatusCode, ApiResponseDto)> GetRoleMenuPermissionsByRoleIdAsync(int RoleId)
        {
            if(RoleId < 1)
                return (HttpStatusCode.BadRequest,
                    new ApiResponseDto { Success = false, Message = AppString.BadRequest, Errors = new List<string> {"Invalid roleId"} });

            var roleMenuPermissionById = await _roleMenuPermission.GetByRoleMenu(RoleId);

            return (HttpStatusCode.OK, new ApiResponseDto { Success = true, Data = roleMenuPermissionById });
        }
    }
}
