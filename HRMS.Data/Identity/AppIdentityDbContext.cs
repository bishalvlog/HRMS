using HRMS.Core.Models.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace HRMS.Data.Identity
{
    public class AppIdentityDbContext : IdentityDbContext<AppDbUser>
    {
        public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options) : base(options) 
        {

        }
    }
}
