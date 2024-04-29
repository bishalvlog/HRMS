using HRMS.Core.Models.Common;
using HRMS.Core.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Services.Users
{
    public interface IUserAccountService
    {
        Task<AppUser> GetByEmailAsync(string email);
        Task<AppUser>GetByUserNameAsync(string userName);
        Task<bool> CheckPasswordAsync(AppUser user, string EnteredPassword);
        Task<BaseResults> UpdateAsync(AppUser user);

    }
}
