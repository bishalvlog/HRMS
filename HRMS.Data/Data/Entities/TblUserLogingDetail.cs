using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Data.Data.Entities
{
    [Table("tbl_user_loging_detail")]
    public class TblUserLogingDetail
    {
        [Key]
        public long Id { get; set; }
        [StringLength(100)]
        public string UserName { get; set; }
        [StringLength(200)]
        public string ClientId { get; set; }
        [Column("RemoteServerIp")]
        [StringLength(50)]
        public string RemoteServerIp { get; set; }
        [Column("ClientDeviceIp")]
        [StringLength(50)]
        public string ClientDeviceIp { get; set; }
        [Column(TypeName = "dateTime")]
        public DateTime? LastLoginLocalTime { get; set; }
        [Column("LastLoginUtcDate", TypeName = "dateTime")]
        public DateTime? LastLoginUtcTime { get; set; }
        [StringLength(100)]
        public string LastLoginNepaliDate { get; set; } 
    }
}
