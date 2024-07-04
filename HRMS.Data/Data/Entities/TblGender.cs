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
    [Table("tbl_gender")]
    public partial class TblGender
    {
        public TblGender()
        {
            TblUsers = new HashSet<TblUser>();
        }

        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        public string GenderName { get; set; }
        [StringLength(20)]
        public string GenderCode { get; set; }
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
        public int? DisplayNumber { get; set; }
        [InverseProperty(nameof(TblUser.GenderNavigation))]
        public virtual ICollection<TblUser> TblUsers { get; set; }
    }
}
