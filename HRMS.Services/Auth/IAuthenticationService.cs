using HRMS.Data.Dtos.Identity;
using HRMS.Data.Dtos.Response;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Services.Auth
{
    public interface IAuthenticationService
    {
        public Task<(HttpStatusCode, ApiResponseDto)> Login(loginDto loginDto);
        

    }
}
