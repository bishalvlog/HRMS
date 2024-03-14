using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Data.Data.Entities
{
    [Index(nameof(RoleId), Name = "IX_AspNetRoleClaims_RoleId")]
      
        public partial class AspNetRoleClaim
        {
            [Key]
            public int Id { get; set; }
            [Required]
            public string RoleId { get; set; }
            public string ClaimType { get; set; }
            public string ClaimValue { get; set; }

            [ForeignKey(nameof(RoleId))]
            [InverseProperty(nameof(AspNetRoles.AspNetRoleClaims))]
            public virtual AspNetRoles Role { get; set; }

        }
    
}
