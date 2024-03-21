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
        [Authorize(Policy = UserPro)]
        public async Task<IActionResult> GetAllMenus()
        {
            var (statusCode, response) = await _menuService.GetMenusAllAsync();

            return GetResponseFromStatusCode(statusCode, response);
        }
    }
}
