﻿using HRMS.Core.Dtos.Users;
using HRMS.Core.Models.Pagging;
using HRMS.Core.Models.SProc;
using HRMS.Core.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Core.Interfaces.Users
{
    public interface IUserRepository
    {
        Task<(SpBaseMessageResponse, bool)> CheckUserExistsAsync(AppUser appUser);
        Task<(SpBaseMessageResponse, AppUser)> GetUserByUserNameAsync(string UserName);
        Task<(SpBaseMessageResponse, AppUser)> CreateUserAsync(AppUser appUser);
        Task<(SpBaseMessageResponse, AppUser)> GetUserByIdAsync(int id);
        Task<(SpBaseMessageResponse,AppUser)> GetUserByEmailAsync(string Email);
        Task<(SpBaseMessageResponse,AppUser)> UpdateUserAsync(AppUser appUser);
        Task<PageResponse<AppUser>> GetUsersAsync(UserListRequest userListRequest);
    }
}
