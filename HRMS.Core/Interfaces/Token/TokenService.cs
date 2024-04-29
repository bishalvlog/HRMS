using HRMS.Core.Domain.Customers;
using HRMS.Core.Models.Auth;
using HRMS.Core.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Core.Interfaces.Token
{
    public interface TokenService
    {
        (string, RefreshToken) CreateUstomerJWtTokenWithRefreshToken(Customer customer, RefreshToken refreshTokenIn = null);
        (string,RefreshToken)CreateUserJwtTokenWithRefreshToken(AppUser appUser, IEnumerable<AppRole> userRoles, RefreshToken refreshTokenIn = null);

    }
}
