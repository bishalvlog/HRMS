using HRMS.Data.Dtos.Response;
using HRMS.Data.Dtos.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Services.Users
{
    public interface IUserAuthService
    {
        Task<(HttpStatusCode, ApiResponseDto)> TokenAsync(UserAuthRequestDto customerAuthDto);
    }
}
