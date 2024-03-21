using Microsoft.AspNetCore.Authentication;

namespace HRMS.Features.Auth.Schema.UserJwt
{
    public class UserJwtBearerAuthenticationOptions : AuthenticationSchemeOptions
    {
        public const string DefaultSchema = "UserJwtBearer";
        public string Schema => DefaultSchema;
        public string AuthenticationType = DefaultSchema;
    }
}
