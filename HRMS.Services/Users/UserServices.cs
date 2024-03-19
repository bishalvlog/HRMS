using AutoMapper;
using HRMS.Core.Comman;
using HRMS.Core.Dtos.Users;
using HRMS.Core.Interfaces.Users;
using HRMS.Core.Models.Pagging;
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
using Microsoft.Identity.Client;
using System.Data;
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

        public async Task<(HttpStatusCode, ApiResponseDto)> GetUserAsync(UserListRequest request)
        {
            var users = await _usersRepository.GetUsersAsync(request);
            var userwithnoCred = _mapper.Map<PageResponse<AppUserOutCred>>(users);
            userwithnoCred.Items = users.Items.Select(user =>
            {
                var userwithnoCreds = _mapper.Map<AppUserOutCred>(users);
                if (!String.IsNullOrWhiteSpace(userwithnoCreds.ProfileImagePath))
                    userwithnoCreds.ProfileImagePath = string.Concat(_config["ApiURL"], userwithnoCreds.ProfileImagePath);

                return userwithnoCreds;
            });
            return (HttpStatusCode.OK, new ApiResponseDto { Success = true,Message = "OK", Data = userwithnoCred });
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
        public async Task<(HttpStatusCode, ApiResponseDto)> UpdateUserAsync(UpdateUserDto updateUserDto)
        {
            var (spMsgResponse, useExists) = await _usersRepository.GetUserByIdAsync(updateUserDto.Id);
            if (spMsgResponse.StatusCode != 200 || useExists is null)
                return (HttpStatusCode.OK, new ApiResponseDto
                {
                    Success = false,
                    Message = AppString.NotFound,
                    Errors = new() { AppString.Forbidden }
                });
            var userToBeUpdate = _mapper.Map(updateUserDto,useExists);
            string imagePath = null;
            if(updateUserDto.ProfileImage != null)
            {
                var folderPath = _config["Folder:UserProfileImage"];
                imagePath = await FileUploadHelper.UploadFile(_hostEnv, folderPath, updateUserDto.ProfileImage);
                userToBeUpdate.ProfileImagePath = imagePath;
            }

            userToBeUpdate.UpdatedBy = _loggedInUser.Claims.FirstOrDefault(x => x.Type == UserClaimTypes.UserName)?.Value;

            var (spMessage, userUpdate) = await _usersRepository.UpdateUserAsync(userToBeUpdate);

             var userUpdatedWithoutCreds = _mapper.Map<AppUserOutCred>(userUpdate);

            if(spMessage.StatusCode != 200)
            {
                if(!String.IsNullOrWhiteSpace(imagePath))
                {
                    var imageToDeletePath = Path.Combine("" + _hostEnv.WebRootPath, imagePath[1..]);
                    File.Delete(imageToDeletePath);
                }
                return GetErrorResponseFromSprocMessage(spMessage);
            }
            return (HttpStatusCode.OK,
                new ApiResponseDto { Success = true,Message = $"User {userToBeUpdate.UserName} update Successfully", Data = userUpdatedWithoutCreds });
        }
       
    }
}
