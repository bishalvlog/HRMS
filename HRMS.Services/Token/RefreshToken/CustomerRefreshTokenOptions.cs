using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalGold.Services.Tokens.RefreshTokens
{
    public class CustomerRefreshTokenOptions
    {
        public const string SectionName = "Authentication:Customer:RefreshToken";
        public int ExpirationMinutes { get; set; }
    }

}
