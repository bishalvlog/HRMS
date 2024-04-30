using HRMS.Core.Domain;
using HRMS.Core.Models.Menu;
using HRMS.Features.Auth.policies;
using HRMS.Mvc.Filters;
using HRMS.Services.Menu;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Controllers
{
    //[Authorize(Policy =UserPolicies.HrmsAdmin)]
    public class MenuManagerController : BaseApiController
    {
        private readonly IMenusService _menuService;
        private readonly IRoleMenuPermissionService _rmsMenuPermission;

        public MenuManagerController(IMenusService menuService, IRoleMenuPermissionService roleMenuPermissionService)
        {
            _menuService = menuService;
            _rmsMenuPermission = roleMenuPermissionService;

        }
        [HttpGet]
        [Route("get-Menus-all")]
        [Authorize(Policy = UserPolicies.HrmsAdmin)]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetAllMenus()
        {
            var (statusCode, response) = await _menuService.GetMenusAllAsync();

            return GetResponseFromStatusCode(statusCode, response);
        }
        [HttpPost]
        [Route("AddMenus")]
        [Authorize(Policy =UserPolicies.HrmsAdmin)]
        [ProducesResponseType(200)]
        [ProducesResponseType(409)]
        [LogUserActivity(UserActionTypes.ActionCreatedMenu)]
        public async Task<IActionResult> AddMenu([FromForm] MenuModel menuModel)
        {
            var (statusCode, response) = await _menuService.AddMenuAsync(menuModel,User);
            return GetResponseFromStatusCode(statusCode, response);
        }
        [HttpGet]
        [Route("get-menu-by-id")]
        [Authorize(Policy =UserPolicies.HrmsAdmin)]
        public async Task<IActionResult> GetRoleMenuPermissonById(int roleId)
        {
            var (statusCode, response) = await _rmsMenuPermission.GetRoleMenuPermissionsByRoleIdAsync(roleId);
            return GetResponseFromStatusCode(statusCode, response);
        }
        [HttpPost]
        [Route("add-Menu-to-role")]
        [Authorize(Policy =UserPolicies.HrmsAdmin)]
        public async Task<IActionResult> AddMenuToRoles(int roleId,[FromBody] IEnumerable<RoleMenuPermissions> permissions)
        {
            var (statusCode, response) = await _rmsMenuPermission.AddRoleMenuPermissionToAsync(roleId,permissions, User);
            return GetResponseFromStatusCode(statusCode, response);
        }
        [HttpPut]
        [Route("update-menu-to-role")]
        [Authorize(Policy =UserPolicies.HrmsAdmin)]
        public async Task<IActionResult> UpdateMenuToRoles(int roleId, [FromBody] IEnumerable<RoleMenuPermissions> permissions)
        {
            var (statusCode, response) = await _rmsMenuPermission.UpdateRoleMenuPermissionToAsync(roleId,permissions, User);
            return GetResponseFromStatusCode(statusCode, response); 
        }
        [HttpGet]
        [Route("Get-Sub-Menu-CurrentUser")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetMenusSubmenusForCurrentUser()
        {
            var (statusCode, response) = await _rmsMenuPermission.GetMenusSubmenuCurrentUserAsync(User);
            return GetResponseFromStatusCode(statusCode, response); 
        }
    }
}
