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
    public class ClientMenuService : IClientMenuService
    {
        private readonly IClientMenuRepository _clientMenuRepo;

        public ClientMenuService(IClientMenuRepository clientMenuRepo)
        {
            _clientMenuRepo = clientMenuRepo;
        }
        public async Task<(HttpStatusCode, ApiResponseDto)> GetMenuSection(string ProductTypeCode)
        {
            var listMenusAll = await _clientMenuRepo.GetListAllAsync(ProductTypeCode);
            return (HttpStatusCode.OK, new ApiResponseDto { Success = true,Data =listMenusAll });
        }

        public async Task<(HttpStatusCode, ApiResponseDto)> getMenuSubsection(int ParentSectionId)
        {
           var listMenusSub = await _clientMenuRepo.GetSubsectionAsync(ParentSectionId);
            return (HttpStatusCode.OK, new ApiResponseDto { Success = true,Data = listMenusSub});
        }
    }
}
