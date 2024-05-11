using HRMS.Features.Auth.Schema.CustomerJwt;
using Microsoft.AspNetCore.Authorization;

namespace HRMS.Features.Auth.policies
{
    public class CustomerPolicies
    {

        #region PolicyNames
        public const string HrmlsCustomer = "HrmlsCustomer";
        #endregion
        private static readonly Dictionary<string, AuthorizationPolicy> _Policies =
            new()
            {
                {
                    HrmlsCustomer,
                    new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(CustomerJwtBearerAuthenticationOptions.DefaultSchema)
                    .RequireAuthenticatedUser()
                    .Build()
                }
            };
        public static Dictionary<string,AuthorizationPolicy> Policies { get =>  _Policies;}
    }
}
