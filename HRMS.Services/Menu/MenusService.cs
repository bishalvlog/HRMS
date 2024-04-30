using HRMS.Core.Interfaces.Menu;
using HRMS.Core.Models.Menu;
using HRMS.Data.Comman.Helpers;
using HRMS.Data.Dtos.Response;
using HRMS.Services.Token;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Security.Claims;

namespace HRMS.Services.Menu
{
    public class MenusService : IMenusService
    {
        private readonly IMenuRepository _MenuRespository;
        private readonly IConfiguration _config;
        private readonly IWebHostEnvironment _hostEnv;
        public readonly string _apiUrl;

        public MenusService(IMenuRepository menuRespository, IConfiguration config, IWebHostEnvironment hostEnv)
        {
            _MenuRespository = menuRespository;
            _config = config;
            _hostEnv = hostEnv;
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

        public async Task<(HttpStatusCode, ApiResponseDto)> AddMenuAsync(MenuModel menus, ClaimsPrincipal claimsPrincipal)
        {
            string imagePath = null;

            if(menus.MenuImage != null)
            {
                var folderPath = _config["Folder:MenuImageFolder"];
                imagePath = await FileUploadHelper.UploadFile(_hostEnv, folderPath, menus.MenuImage);
            }

            var menusToAdd = new MenuAddUpdate
            {
                Title = menus.Title,
                ParentId = menus.ParentId,
                MenuUrl = menus.MenuUrl,
                IsActive = menus.IsActive,
                DisplayOrder = menus.DisplayOrder,
                ImagePath = imagePath,
                CreatedBy = claimsPrincipal.FindFirstValue(UserClaimTypes.UserName)
            };
            var resultSet = await _MenuRespository.AddAsync(menusToAdd);
            if(resultSet == 409)
            {
                if (!String.IsNullOrWhiteSpace(imagePath))
                {
                    var imageToDeletePath = Path.Combine("" + _hostEnv.WebRootPath, imagePath[1..]);
                    File.Delete(imageToDeletePath);
                }
                return (HttpStatusCode.Conflict,
                    new ApiResponseDto
                    {
                        Success = false,
                        Message = "Conflict !",
                        Errors = new List<string> { $"menu with the same title {menus.Title} and parent Id {menus.ParentId} already exists !" }

                    });
            }

            return (HttpStatusCode.OK, new ApiResponseDto { Success = true, Message = "Added Successfully" });

        }
    }
}
