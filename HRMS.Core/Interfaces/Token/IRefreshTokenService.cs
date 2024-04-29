using HRMS.Core.Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Core.Interfaces.Token
{
    public interface  IRefreshTokenService
    {
        Task<HttpStatusCode> CreateAsync(RefreshToken refreshToken);
        Task<RefreshToken> GetAsync(string token);
        Task<HttpStatusCode>UpdateAsync (RefreshToken refreshToken);    
    }
}
