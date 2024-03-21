using AutoMapper;
using HRMS.Core.Interfaces.Menu;
using HRMS.Data.Dtos.Response;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Services.Menu
{
    public class MenusService : IMenusService
    {
        private readonly IMenuRepository _MenuRespository;
        private readonly IConfiguration _config;
        private readonly IWebHostEnvironment _hostEnv;
        private readonly IMapper _mapper;
        public readonly string _apiUrl;

        public MenusService(IMenuRepository menuRespository, IConfiguration config, IWebHostEnvironment hostEnv, IMapper mapper, string apiUrl)
        {
            _MenuRespository = menuRespository;
            _config = config;
            _hostEnv = hostEnv;
            _mapper = mapper;
            _apiUrl = _config["ApiURL"];
        }

        public async Task<(HttpStatusCode, ApiResponseDto)> GetMenusAllAsync()
        {
           var listMenuAll = await _MenuRespository.GetListAllAsync();

            listMenuAll = listMenuAll.Select(m =>
            {
                if (!String.IsNullOrWhiteSpace(m.ImagePath))
                    m.ImagePath = string.Join(string.Empty, _apiUrl, m.ImagePath);
                return m;
            });

            return (HttpStatusCode.OK, new ApiResponseDto { Success = true, Data = listMenuAll });
        }
    }
}
