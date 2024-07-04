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
    [Table("tbl_roles")]
    [Index(nameof(RoleName), IsUnique = true)]
    public partial class TblRole
    {
        public TblRole()
        {
            TblRoleMenuPermissions = new HashSet<TblRoleMenuPermission>();
           
        }
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string RoleName { get; set; }
        [StringLength(300)]
        public string Description { get; set; }
        public bool? IsActive { get; set; }
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

        [InverseProperty(nameof(TblRoleMenuPermission.Role))]
        public virtual ICollection<TblRoleMenuPermission> TblRoleMenuPermissions { get; set; }
    }
}
