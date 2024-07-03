using HRMS.Data.Dtos.Response;
using HRMS.Data.Dtos.RolesDto;
using HRMS.Data.Dtos.UserDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Services.Users
{
    public interface IRoleServices
    {
        Task<(HttpStatusCode, ApiResponseDto)> CreateRolesAsync(CreateRolesDto createRolesDto);
        Task<(HttpStatusCode, ApiResponseDto)> GetRolesByIdAsync(int id);
        Task<(HttpStatusCode, ApiResponseDto)> GetRolesAsync();
        Task<(HttpStatusCode, ApiResponseDto)> UpdateRolesAsync(UpdateRoleDto update);
        Task<(HttpStatusCode, ApiResponseDto)> DeleteRoleAsync(int id);

    }
}
