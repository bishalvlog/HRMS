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
    [Table("tbl_role_menu_permissions")]
    [Index(nameof(RoleId), nameof(MenuId), IsUnique = true)]
    public partial class TblRoleMenuPermission
    {
        [Key]
        public int Id { get; set; }
        public int RoleId { get; set; }
        public int MenuId { get; set; }
        [Required]
        public bool? ViewPer { get; set; }
        [Required]
        public bool? CreatePer { get; set; }
        [Required]
        public bool? UpdatePer { get; set; }
        [Required]
        public bool? DeletePer { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedLocalDate { get; set; }
        [Column("CreatedUTCDate", TypeName = "datetime")]
        public DateTime? CreatedUtcdate { get; set; }
        [StringLength(10)]
        public string CreatedNepaliDate { get; set; }
        [StringLength(100)]
        public string CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedLocalDate { get; set; }
        [Column("UpdatedUTCDate", TypeName = "datetime")]
        public DateTime? UpdatedUtcdate { get; set; }
        [StringLength(10)]
        public string UpdatedNepaliDate { get; set; }
        [StringLength(100)]
        public string UpdatedBy { get; set; }
        public bool IsActive { get; set; }

        [ForeignKey(nameof(MenuId))]
        [InverseProperty(nameof(TblMenu.TblRoleMenuPermissions))]
        public virtual TblMenu Menu { get; set; }
        [ForeignKey(nameof(RoleId))]
        [InverseProperty(nameof(TblRole.TblRoleMenuPermissions))]
        public virtual TblRole Role { get; set; }
    }
}
