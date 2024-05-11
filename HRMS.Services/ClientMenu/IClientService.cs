using HRMS.Data.Dtos.Response;
using HRMS.Data.Repository.ClientMenu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Services.ClientMenu
{
    public interface IClientMenuService
    {
        Task<(HttpStatusCode, ApiResponseDto)> GetMenuSection(string ProductTypeCode);
        Task<(HttpStatusCode, ApiResponseDto)> getMenuSubsection(int ParentSectionId);
    }
}
