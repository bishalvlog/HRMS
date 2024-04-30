using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Services.Token.jwt
{
    public class CustomerJwtOptions : JwtOptions
    {
        public const string JwtOption = "Authentication.customer.jwt";
    }
}
