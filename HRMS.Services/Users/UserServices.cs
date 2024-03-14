using AutoMapper;
using HRMS.Core.Comman;
using HRMS.Core.Interfaces.Users;
using HRMS.Core.Models.Users;
using HRMS.Data.Comman.Constrant;
using HRMS.Data.Comman.Helpers;
using HRMS.Data.Dtos.Response;
using HRMS.Data.Dtos.UserDto;
using HRMS.Services.Commond;
using HRMS.Services.Token;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Security.Claims;

namespace HRMS.Services.Users
{
    public class UserServices :BaseService, IUserServices
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ClaimsPrincipal _loggedInUser;
        private readonly IConfiguration _config;
        private readonly IWebHostEnvironment _hostEnv;
        private readonly IMapper _mapper;
        private readonly IUserRepository _usersRepository;
        //private readonly IUserAccountService _userAccountService;
        public UserServices(
            IHttpContextAccessor httpContextAccessor,
            IConfiguration config,
            IWebHostEnvironment hostEnv,
            IMapper mapper,
            IUserRepository usersRepository)
            {
                _httpContextAccessor = httpContextAccessor;
                _loggedInUser = httpContextAccessor.HttpContext.User;
                _config = config;
                _hostEnv = hostEnv;
                _mapper = mapper;
                _usersRepository = usersRepository;
           
            }

        public async Task<(HttpStatusCode, ApiResponseDto)> CreateUserAsync(CreateUserDto createUserDto)
        {
            createUserDto.UserName = createUserDto.UserName?.ToLower();
            var (spMsgResponse, userExists) = await _usersRepository.CheckUserExistsAsync(_mapper.Map<AppUser>(createUserDto));
            if(userExists)
                return (HttpStatusCode.Conflict,
                    new ApiResponseDto {  Success =  true, Message =AppString.Conflict, Errors = new List<string> { spMsgResponse.MsgText} });

            var userToBeRegister = _mapper.Map<AppUser>(createUserDto);
            string imagepath = null;
            if(createUserDto.ProfileImage != null)
            {
                var folderPath = _config["Folder:UserProfileImage"];
                imagepath = await FileUploadHelper.UploadFile(_hostEnv, folderPath, createUserDto.ProfileImage);
                userToBeRegister.ProfileImagePath = imagepath;
            }
            userToBeRegister.PasswordSalt = CryptoUtils.GenerateKeySalt(128);
            userToBeRegister.PasswordHash = CryptoUtils.HashHmacsha512Base64(createUserDto.Password, userToBeRegister.PasswordSalt);
            userToBeRegister.CreatedBy = _loggedInUser.Claims.FirstOrDefault(x => x.Type == UserClaimTypes.UserName)?.Value;

            var (spMsgResponseReq, create) = await _usersRepository.CreateUserAsync(userToBeRegister);

            if(spMsgResponse.StatusCode != 200)
            {
                if (!string.IsNullOrEmpty(imagepath))
                {
                    var imagToBeDelete = Path.Combine("" + _hostEnv.WebRootPath, imagepath[1..]);
                    File.Delete(imagToBeDelete);
                }
                return GetErrorResponseFromSprocMessage(spMsgResponseReq);
            }
            var userCreateWithOutCreds = _mapper.Map<AppUserOutCred>(create);
            return
                (HttpStatusCode.OK, new ApiResponseDto { Success = true, Message = $"User {create.UserName} Registerd Successfully", Data = userCreateWithOutCreds });
        }

        public async Task<(HttpStatusCode, ApiResponseDto)> GetUserByIdAsync(int userId)
        {
            var (msgResponse, user) = await _usersRepository.GetUserByIdAsync(userId);
            if(msgResponse.StatusCode !=200) return GetErrorResponseFromSprocMessage(msgResponse);
            var userWithOutCreds = _mapper.Map<AppUserOutCred>(user);

            if (!string.IsNullOrWhiteSpace(userWithOutCreds.ProfileImagePath))
                userWithOutCreds.ProfileImagePath = string.Concat(_config["ApiURL"], userWithOutCreds.ProfileImagePath);
            return (HttpStatusCode.OK,new ApiResponseDto { Success = true, Message = "OK", Data = userWithOutCreds });
        }
    }
}
