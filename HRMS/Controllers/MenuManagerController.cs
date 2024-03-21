using HRMS.Core.Domain;
using HRMS.Core.Models.Menu;
using HRMS.Features.Auth.policies;
using HRMS.Mvc.Filters;
using HRMS.Services.Menu;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Controllers
{
    public class MenuManagerController : BaseApiController
    {
        private readonly IMenusService _menuService;

        public MenuManagerController(IMenusService menuService)
        {
            _menuService = menuService;
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
    }
}
