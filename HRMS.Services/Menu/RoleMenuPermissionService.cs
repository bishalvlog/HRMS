using HRMS.Core.Interfaces.Menu;
using HRMS.Core.Models.Menu;
using HRMS.Data.Comman.Constrant;
using HRMS.Data.Comman.Helpers;
using HRMS.Data.Dtos.Response;
using HRMS.Data.Repository.Menu.Types;
using HRMS.Services.Token;
using Microsoft.AspNetCore.Server.HttpSys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
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

        public async Task<(HttpStatusCode, ApiResponseDto)> AddRoleMenuPermissionToAsync(int roleId, IEnumerable<RoleMenuPermissions> rmpAddRoles, ClaimsPrincipal user)
        {
            if (roleId < 1)
                return (HttpStatusCode.BadRequest,
                    new ApiResponseDto { Success = false, Message = AppString.BadRequest, Errors = new List<string> { "Invalid RoleId!" } });
            var updatedLocalDate = DateTime.Now;
            var updatedUTCDate = DateTime.Now;
            var updatedNepaliDate = DateConversions.ConvertToNepaliDate(DateTime.Now);
            var updateBy = user?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;

            var listOfRoleMenuPermission = rmpAddRoles.Select(x => new RoleMenuPermissionTypes
            {
                MenuId = x.MenuId,
                ViewPer = x.ViewPer,
                CreatePer = x.CreatePer,
                UpdatePer = x.UpdatePer,
                DeletePer = x.DeletePer,
                UpdatedLocalDate = updatedLocalDate,
                UpdatedUTCDate = updatedUTCDate,
                UpdatedNepaliDate = updatedNepaliDate,
                UpdatedBy = updateBy
            });
            var resultsStatus = await _roleMenuPermission.AddListAsync(roleId, listOfRoleMenuPermission);
            if (resultsStatus < 0)
                return (HttpStatusCode.BadRequest, new ApiResponseDto { Success = false, Message = AppString.BadRequest, Errors = new List<string> { "Failed to Add Role" } });

            return (HttpStatusCode.OK, new ApiResponseDto { Success = true, Message = "Role Add Successfully" });
        }

        public async Task<(HttpStatusCode, ApiResponseDto)> GetMenusSubmenuCurrentUserAsync(ClaimsPrincipal usr)
        {
            var userName = usr?.FindFirstValue(UserClaimTypes.UserName);
            if (userName is null)
                return (HttpStatusCode.Unauthorized, new ApiResponseDto { Success = false, Message = AppString.Unauthorized, Errors = new List<string> { "Invalid Users" } });
            var menusWithSubmenus = await _roleMenuPermission.GetListWithSubMenusAsync(userName);
            return (HttpStatusCode.OK, new ApiResponseDto { Success = true, Data = menusWithSubmenus});
        }

        public async Task<(HttpStatusCode, ApiResponseDto)> GetRoleMenuPermissionsByRoleIdAsync(int RoleId)
        {
            if(RoleId < 1)
                return (HttpStatusCode.BadRequest,
                    new ApiResponseDto { Success = false, Message = AppString.BadRequest, Errors = new List<string> {"Invalid roleId"} });

            var roleMenuPermissionById = await _roleMenuPermission.GetByRoleMenu(RoleId);

            return (HttpStatusCode.OK, new ApiResponseDto { Success = true, Data = roleMenuPermissionById });
        }

        public async Task<(HttpStatusCode, ApiResponseDto)> UpdateRoleMenuPermissionToAsync(int roleId, IEnumerable<RoleMenuPermissions> rmpRoleMenu, ClaimsPrincipal user)
        {
            if (roleId < 1)
                return (HttpStatusCode.BadRequest,
                        new ApiResponseDto { Success = false, Message = AppString.BadRequest, Errors = new List<string> { "Invalid roleId" } });
            var UpdatedLocalDate = DateTime.Now;
            var UpdatedUTCDate = DateTime.UtcNow;
            var updatedNepaliDate = DateConversions.ConvertToNepaliDate(DateTime.Now);
            var updatedBy = user?.Claims?.FirstOrDefault(x => x.Type ==ClaimTypes.Name)?.Value;

            var lisRoleMenuPermission = rmpRoleMenu.Select(x => new RoleMenuPermissionTypes
            {
                MenuId = x.MenuId,
                ViewPer = x.ViewPer,
                CreatePer = x.CreatePer,
                UpdatePer = x.UpdatePer,
                DeletePer = x.DeletePer,
                UpdatedLocalDate = UpdatedLocalDate,
                UpdatedUTCDate = UpdatedUTCDate,
                UpdatedNepaliDate = updatedNepaliDate,
                UpdatedBy = updatedBy
            });
            var resultsStatus = await _roleMenuPermission.UpdateListAsync(roleId, lisRoleMenuPermission);
            if (resultsStatus < 0)
                return (HttpStatusCode.BadRequest, new ApiResponseDto { Success = false, Message = AppString.BadRequest, Errors = new List<string> { "Failed to Updated" } });
            return (HttpStatusCode.OK, new ApiResponseDto { Success = true, Message = "Update Successfully" });
        }
    }
}
