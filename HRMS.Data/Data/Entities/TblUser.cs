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
    [Table("tbl_users")]
    [Index(nameof(UserName), IsUnique = true)]
    public partial class TblUser
    {

        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string UserName { get; set; }
        [StringLength(200)]
        public string FullName { get; set; }
        [StringLength(200)]
        public string Email { get; set; }
        [StringLength(10)]
        public string Mobile { get; set; }
        [StringLength(500)]
        public string Address { get; set; }
        public int? Gender { get; set; }
        [StringLength(100)]
        public string Department { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DateOfBirth { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DateOfJoining { get; set; }
        [StringLength(500)]
        public string ProfileImagePath { get; set; }
        [StringLength(50)]
        public string AccessCode { get; set; }
        public string PasswordHash { get; set; }
        [StringLength(500)]
        public string PasswordSalt { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsSuperAdmin { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LastLoginDate { get; set; }
        [StringLength(100)]
        public string CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedLocalDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedUtcDate { get; set; }
        [StringLength(10)]
        public string CreatedNepaliDate { get; set; }
        [StringLength(100)]
        public string UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedLocalDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedUtcDate { get; set; }
        [StringLength(10)]
        public string UpdatedNepaliDate { get; set; }

        [ForeignKey(nameof(Gender))]
        [InverseProperty(nameof(TblGender.TblUsers))]
        public virtual TblGender GenderNavigation { get; set; }

    }
}
