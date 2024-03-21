using HRMS.Services.Token;
using System.Security.Claims;

namespace HRMS.Extensions
{
    public static class ClaimPrincipalExtensions
    {
        public static bool IsSuperAdmin(this ClaimsPrincipal users)
        {
            return users.HasClaim(UserClaimTypes.IsSuperAdmin, true.ToString());
        }
        public static bool IsSameCustomer(this ClaimsPrincipal users, string customerId)
        {
            if(customerId is null)
                throw new ArgumentNullException(nameof(customerId));
                return users.HasClaim(CustomerClaimTypes.CustomerId, customerId);
        }

    }
}
