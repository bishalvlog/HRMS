using HRMS.Data.Dtos.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Services.Menu
{
    public interface IMenusService
    {
        public Task<(HttpStatusCode, ApiResponseDto)> GetMenusAllAsync();
    }
}
