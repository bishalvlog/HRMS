using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Bcpg.OpenPgp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Services.Token.jwt
{
    public class JwtOptions
    {
        public string validUser {  get; set; }  
        public string validAudience { get; set; }   
        public string secretKey { get; set; }   
        public int ExpirationMinutes { get; set; }

        public SymmetricSecurityKey IssueSignKey => new(Encoding.UTF8.GetBytes(secretKey));
    }
}
