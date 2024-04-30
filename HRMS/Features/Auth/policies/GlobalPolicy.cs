using Microsoft.AspNetCore.Authorization;

namespace HRMS.Features.Auth.policies
{
    public class GlobalPolicy
    {
        #region policynames

        private static readonly Dictionary<string, AuthorizationPolicy> _policies = new()
        {

        };
        #endregion
        public static Dictionary<string, AuthorizationPolicy> Policies { get => _policies; }
    }
}
