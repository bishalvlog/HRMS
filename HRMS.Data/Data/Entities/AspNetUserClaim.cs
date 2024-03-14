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
        [Index(nameof(UserId), Name = "IX_AspNetUserClaims_UserId")]
        public partial class AspNetUserClaim
        {
            [Key]
            public int Id { get; set; }
            [Required]
            public string UserId { get; set; }
            public string ClaimType { get; set; }
            public string ClaimValue { get; set; }
            [ForeignKey(nameof(UserId))]
            [InverseProperty(nameof(AspNetUser.AspNetUserClaims))]
            public virtual AspNetUser User { get; set; }
        }  
}
