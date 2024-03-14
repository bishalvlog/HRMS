using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Data.Data.Entities
{
        [Index(nameof(NormalizedEmail), Name = "EmailIndex")]
        public partial class AspNetUser
        {
            public AspNetUser()
            {
                AspNetUserClaims = new HashSet<AspNetUserClaim>();
                AspNetUserLogins = new HashSet<AspNetUserLogin>();
                AspNetUserRoles = new HashSet<AspNetUserRole>();
                AspNetUserTokens = new HashSet<AspNetUserToken>();
            }

            [Key]
            public string Id { get; set; }
            [StringLength(256)]
            public string UserName { get; set; }
            [StringLength(256)]
            public string NormalizedUserName { get; set; }
            [StringLength(256)]
            public string Email { get; set; }
            [StringLength(256)]
            public string NormalizedEmail { get; set; }
            public bool EmailConfirmed { get; set; }
            public string PasswordHash { get; set; }
            public string SecurityStamp { get; set; }
            public string ConcurrencyStamp { get; set; }
            public string PhoneNumber { get; set; }
            public bool PhoneNumberConfirmed { get; set; }
            public bool TwoFactorEnabled { get; set; }
            public DateTimeOffset? LockoutEnd { get; set; }
            public bool LockoutEnabled { get; set; }
            public int AccessFailedCount { get; set; }
            public DateTime? DateOfBirth { get; set; }
            [StringLength(200)]
            public string FullName { get; set; }
            [StringLength(500)]
            public string Address { get; set; }
            public DateTime? DateOfJoining { get; set; }
            [StringLength(200)]
            public string Department { get; set; }
            [StringLength(20)]
            public string Gender { get; set; }
            [StringLength(20)]
            public string MobileNumber { get; set; }
            public bool? IsActive { get; set; }
            [StringLength(500)]
            public string ProfileImgagePath { get; set; }
            [StringLength(100)]
            public string CreatedBy { get; set; }
            public DateTime? CreatedLocalDate { get; set; }
            [StringLength(10)]
            public string CreatedNepaliDate { get; set; }
            [Column("CreatedUTCDate")]
            public DateTime? CreatedUtcdate { get; set; }
            [StringLength(100)]
            public string UpdatedBy { get; set; }
            public DateTime? UpdatedLocalDate { get; set; }
            [StringLength(10)]
            public string UpdatedNepaliDate { get; set; }
            [Column("UpdatedUTCDate")]
            public DateTime? UpdatedUtcdate { get; set; }
            public bool? IsCustomer { get; set; }

            [InverseProperty(nameof(AspNetUserClaim.User))]
            public virtual ICollection<AspNetUserClaim> AspNetUserClaims { get; set; }
            [InverseProperty(nameof(AspNetUserLogin.User))]
            public virtual ICollection<AspNetUserLogin> AspNetUserLogins { get; set; }
            [InverseProperty(nameof(AspNetUserRole.User))]
            public virtual ICollection<AspNetUserRole> AspNetUserRoles { get; set; }
            [InverseProperty(nameof(AspNetUserToken.User))]
            public virtual ICollection<AspNetUserToken> AspNetUserTokens { get; set; }
        }
    
}
