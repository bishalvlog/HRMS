using HRMS.Core.Models.Menu;
using HRMS.Data.Dtos.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Services.Menu
{
    public interface IRoleMenuPermissionService
    {
        Task<(HttpStatusCode, ApiResponseDto)> GetRoleMenuPermissionsByRoleIdAsync(int RoleId);
        Task<(HttpStatusCode, ApiResponseDto)> AddRoleMenuPermissionToAsync(int roleId, IEnumerable<RoleMenuPermissions> rmpAddRoles, ClaimsPrincipal user);
        Task<(HttpStatusCode,ApiResponseDto)> UpdateRoleMenuPermissionToAsync(int roleId, IEnumerable<RoleMenuPermissions> rmpRoleMenu, ClaimsPrincipal user);
        Task<(HttpStatusCode, ApiResponseDto)> GetMenusSubmenuCurrentUserAsync(ClaimsPrincipal usr);
    }
}
