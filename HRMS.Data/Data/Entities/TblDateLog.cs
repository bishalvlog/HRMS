using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace HRMS.Data.Data.Entities
{
    [Table("tbl_date_log")]
    public partial class TblDateLog
    {
        [Key]
        [Column("dt_sno")]
        public int DtSno { get; set; }
        [Column("nepali_year")]
        public int? NepaliYear { get; set; }
        [Column("nepali_month")]
        public byte? NepaliMonth { get; set; }
        [Column("english_date", TypeName = "date")]
        public DateTime? EnglishDate { get; set; }
        [Column("created_by", TypeName = "date")]
        public DateTime? CreatedBy { get; set; }
        [Column("created_UTC_date", TypeName = "date")]
        public DateTime? CreatedUtcDate { get; set; }
        [Column("created_local_date", TypeName = "date")]
        public DateTime? CreatedLocalDate { get; set; }
    }
}
