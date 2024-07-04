using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Data.Data.Entities
{
    [Table("")]
    public partial class TblClientList
    {
        [Key]
        [StringLength(15)]
        public string CompanyCode { get; set; }
        [StringLength(200)]
        public string CompanyName { get; set; }
        [StringLength(100)]
        public string UserName { get; set; }
        [StringLength(500)]
        public string PasswordHash { get; set; }
        [StringLength(500)]
        public string PasswordSalt { get; set; }
        [StringLength(200)]
        public string Email { get; set; }
        [StringLength(20)]
        public string PhoneNumber { get; set; }
        [StringLength(300)]
        public string Address { get; set; }
        public bool? Status { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedLocalDate { get; set; }
        [Column("CreatedUTCDate", TypeName = "datetime")]
        public DateTime? CreatedUtcdate { get; set; }
        [StringLength(10)]
        public string CreatedNepaliDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedLocalDate { get; set; }
        [Column("UpdatedUTCDate", TypeName = "datetime")]
        public DateTime? UpdatedUtcdate { get; set; }
        [StringLength(10)]
        public string UpdatedNepaliDate { get; set; }
        [StringLength(50)]
        public string Role { get; set; }

    }
}
