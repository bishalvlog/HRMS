using HRMS.Core.Domain.Users;
using HRMS.Extensions;
using HRMS.Features.Auth.Schema.UserJwt;
using Microsoft.AspNetCore.Authorization;

namespace HRMS.Features.Auth.policies
{
    public static class UserPolicies
    {
        #region
        public const string HrmsSuperAdmin = "HrmsSuperAdmin";
        public const string HrmsAdmin = "HrmsAdmin";
        public const string HrmsUser = "HrmsUser";
        #endregion

        private static readonly Dictionary<string, AuthorizationPolicy> _policies = new()
        {
            {
                HrmsSuperAdmin,
                new AuthorizationPolicyBuilder()
                .AddAuthenticationSchemes(UserJwtBearerAuthenticationOptions.DefaultSchema)
                .RequireAuthenticatedUser()
                .RequireAssertion(c => c.User.IsSuperAdmin())
                .Build()

            },
            {
                HrmsAdmin,
                new AuthorizationPolicyBuilder()
                .AddAuthenticationSchemes(UserJwtBearerAuthenticationOptions.DefaultSchema)
                .RequireAuthenticatedUser()
                .RequireAssertion(c => c.User.IsInRole(UserRoles.Admin) || c.User.IsSuperAdmin())
                .Build()
            },
            {
                HrmsUser,
                new AuthorizationPolicyBuilder()
                .AddAuthenticationSchemes(UserJwtBearerAuthenticationOptions.DefaultSchema)
                .RequireAuthenticatedUser()
                .Build()
            }
        };
        public static Dictionary<string, AuthorizationPolicy> Policies { get {  return _policies; } }

    }
}
