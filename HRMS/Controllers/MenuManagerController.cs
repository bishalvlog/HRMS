using HRMS.Core.Domain;
using HRMS.Core.Domain.Customers;
using HRMS.Core.Models.Menu;
using HRMS.Data.Repository.ClientMenu;
using HRMS.Features.Auth.policies;
using HRMS.Mvc.Filters;
using HRMS.Services.ClientMenu;
using HRMS.Services.Menu;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Controllers
{
    [Authorize(Policy =UserPolicies.HrmsAdmin)]
    public class MenuManagerController : BaseApiController
    {
        private readonly IClientMenuService _clientService;
        private readonly IMenusService _menuService;
        private readonly IRoleMenuPermissionService _rmsMenuPermission;

        public MenuManagerController(IMenusService menuService, IRoleMenuPermissionService roleMenuPermissionService, IClientMenuService clientMenuService)
        {
            _clientService = clientMenuService;
            _menuService = menuService;
            _rmsMenuPermission = roleMenuPermissionService;

        }
        [HttpGet]
        [Route("get-Client-menu-Section")]
        [Authorize(Policy = CustomerPolicies.HrmlsCustomer)]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetMenusClientSection (string ProductTypesCode)
        {
            var (statusCode, response) = await _clientService.GetMenuSection(ProductTypesCode);
            return GetResponseFromStatusCode(statusCode, response);
        }
        [HttpGet]
        [Route("get-Client-menu-SubSection")]
        [Authorize(Policy = CustomerPolicies.HrmlsCustomer)]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetMenusClientSubSection (int ParentSectionId)
        {
            var (statusCode, response)  = await _clientService.getMenuSubsection(ParentSectionId);
            return GetResponseFromStatusCode(statusCode, response);
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
