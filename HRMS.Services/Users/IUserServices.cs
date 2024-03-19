using HRMS.Core.Dtos.Users;
using HRMS.Data.Dtos.Response;
using HRMS.Data.Dtos.UserDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Services.Users
{
    public interface IUserServices
    {
        Task<(HttpStatusCode, ApiResponseDto)> GetUserByIdAsync(int userId);
        Task<(HttpStatusCode, ApiResponseDto)> CreateUserAsync(CreateUserDto createUserDto);
        Task<(HttpStatusCode, ApiResponseDto)> UpdateUserAsync(UpdateUserDto updateUserDto);
        Task<(HttpStatusCode, ApiResponseDto)> GetUserAsync(UserListRequest request);
    }
}
