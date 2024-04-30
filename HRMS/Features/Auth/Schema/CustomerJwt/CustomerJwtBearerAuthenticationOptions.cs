using Microsoft.AspNetCore.Authentication;

namespace HRMS.Features.Auth.Schema.CustomerJwt
{
    public class CustomerJwtBearerAuthenticationOptions : AuthenticationSchemeOptions
    {
        public const string DefaultSchema = "CustomerBearer";
        public string Schema => DefaultSchema;

        public string AuthenticationType = DefaultSchema;
    }
}
