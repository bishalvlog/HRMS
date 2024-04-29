using HRMS.Core.Comman;
using HRMS.Core.Interfaces.Users;
using HRMS.Core.Models.Common;
using HRMS.Core.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Services.Users
{
    public class UserAccountService : IUserAccountService
    {
        private readonly IUserRepository _userRepository;
        public UserAccountService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> CheckPasswordAsync(AppUser user, string EnteredPassword)
        {
            if(user is null)
            throw new ArgumentNullException(nameof(user));
            if(string.IsNullOrEmpty(EnteredPassword))
                throw new ArgumentNullException(nameof(EnteredPassword));

            if(user.PasswordHash is null || user.PasswordSalt is null)
                return await Task.FromResult(false);

            return await Task.FromResult(CryptoUtils.checkEqualHashHmacSha512(EnteredPassword,user.PasswordHash,user.PasswordSalt));
            
        }

        public async Task<AppUser> GetByEmailAsync(string email)
        {
            if(string.IsNullOrWhiteSpace(email)) throw new ArgumentNullException(nameof(email));

            var (_,user) = await _userRepository.GetUserByEmailAsync(email);

            return user;
        }

        public async Task<AppUser> GetByUserNameAsync(string userName)
        {
            if(string.IsNullOrWhiteSpace(userName)) throw new ArgumentNullException(nameof(userName));

            var (_,user) = await  _userRepository.GetUserByUserNameAsync(userName);

            return user;
        }

        public async Task<BaseResults> UpdateAsync(AppUser user)
        {
            if(user is null)
                throw new ArgumentNullException(nameof(user));  

            var (spMsg, _) = await _userRepository.UpdateUserAsync(user);

            var results = new BaseResults();

            if (spMsg.StatusCode != 200)
                results.AddError(spMsg.MsgText);

            return results; 
        }
    }
}
