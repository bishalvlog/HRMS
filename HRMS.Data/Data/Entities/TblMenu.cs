using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Data.Data.Entities
{
    [Table("tbl_menu")]
    [Index(nameof(Title), nameof(ParentId), IsUnique = true)]
    public partial  class TblMenu
    {
        public TblMenu()
        {
            TblRoleMenuPermissions = new HashSet<TblRoleMenuPermission>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public int ParentId { get; set; }
        [Required]
        [StringLength(2083)]
        public string MenuUrl { get; set; }
        public bool? IsActive { get; set; }
        public int? DisplayOrder { get; set; }
        public string ImagePath { get; set; }
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

        [InverseProperty(nameof(TblRoleMenuPermission.Menu))]
        public virtual ICollection<TblRoleMenuPermission> TblRoleMenuPermissions { get; set; }
    }
}
