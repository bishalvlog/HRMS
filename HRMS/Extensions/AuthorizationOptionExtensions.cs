using HRMS.Features.Auth.policies;
using Microsoft.AspNetCore.Authorization;
using System.Runtime.CompilerServices;

namespace HRMS.Extensions
{
    public static  class AuthorizationOptionExtensions
    {
        public static AuthorizationOptions AddApplicationPolicies (this  AuthorizationOptions options)
        {
            foreach (var policy in GlobalPolicy.Policies)
                options.AddPolicy(policy.Key, policy.Value);

            foreach(var policy in UserPolicies.Policies)
                options.AddPolicy(policy.Key,policy.Value);


            return options;
        } 
    }
}
