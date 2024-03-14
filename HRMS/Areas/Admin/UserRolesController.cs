using HRMS.Controllers;
using HRMS.Core.Domain;
using HRMS.Core.Interfaces.Users;
using HRMS.Data.Dtos.RolesDto;
using HRMS.Data.Repository.Users;
using HRMS.Mvc.Filters;
using HRMS.Services.Users;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace HRMS.Areas.Admin
{
    public class UserRolesController : BaseApiController
    {
        private readonly IRoleServices _roleServices;

        public UserRolesController (IRoleServices roles)
        {
            _roleServices = roles;
        }
        [HttpPost("create-role")]
        [LogUserActivity(UserActionTypes.ActionCreatedRole)]
        public async Task<IActionResult> CreateRole([FromBody] CreateRolesDto createRolesDto)
        {
            var (status, response) = await _roleServices.CreateRolesAsync(createRolesDto);
            return GetResponseFromStatusCode(status, response);
        }
        [HttpGet("GetRoleById")]
        public async Task<IActionResult> GetRoleById([Required] int roleId)
        {
            var (status, response) = await _roleServices.GetRolesByIdAsync(roleId);
            return GetResponseFromStatusCode(status, response);
        }
        [HttpGet("get_roles_all")]
        public async Task<IActionResult> GetRolesAsync()
        {
            var (status, response) = await _roleServices.GetRolesAsync();
            return GetResponseFromStatusCode(status, response);

        }
        [HttpPut("Roles_Updates")]
        [LogUserActivity(UserActionTypes.ActionUpdatedRole)]
        public async Task<IActionResult>UpdateRolesAsync (UpdateRoleDto updateRolesDto)
        {
            var (status, response) = await _roleServices.UpdateRolesAsync(updateRolesDto);
            return GetResponseFromStatusCode(status, response);
        }

    }
}
