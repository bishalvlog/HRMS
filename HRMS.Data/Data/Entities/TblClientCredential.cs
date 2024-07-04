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
        [Keyless]
        [Table("tbl_client_credentials")]
        public partial class TblClientCredential
        {
            [StringLength(15)]
            public string CompanyCode { get; set; }
            [StringLength(200)]
            public string ClientId { get; set; }
            [StringLength(200)]
            public string ClientSecret { get; set; }
            [Column("IPAddress")]
            [StringLength(200)]
            public string Ipaddress { get; set; }
            [StringLength(50)]
            public string AppEnvironment { get; set; }
            public bool? IsActive { get; set; }
            [StringLength(100)]
            public string CreatedBy { get; set; }
            [Column(TypeName = "datetime")]
            public DateTime? CreatedLocalDate { get; set; }
            [Column("CreatedUTCDate", TypeName = "datetime")]
            public DateTime? CreatedUtcdate { get; set; }
            [StringLength(10)]
            public string CreatedNepaliDate { get; set; }
            [StringLength(100)]
            public string UpdatedBy { get; set; }
            [Column(TypeName = "datetime")]
            public DateTime? UpdatedLocalDate { get; set; }
            [Column("UpdatedUTCDate", TypeName = "datetime")]
            public DateTime? UpdatedUtcdate { get; set; }
            [StringLength(10)]
            public string UpdatedNepaliDate { get; set; }
        }
}
