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
    [Table("")]
    [Index(nameof(Email), Name = "", IsUnique = true)]
    public partial class TblCustomer
    {
        [Key]
        [StringLength(12)]
        public string CustomerId { get; set; }
        [StringLength(200)]
        public string FullName { get; set; }
        [Required]
        [StringLength(100)]
        public string Email { get; set; }
        public bool? EmailConfirmed { get; set; }
        [StringLength(10)]
        public string Mobile { get; set; }
        public bool? MobileConfirmed { get; set; }
        public string PasswordHash { get; set; }
        [StringLength(500)]
        public string PasswordSalt { get; set; }
        [Column("MPINHash")]
        public string Mpinhash { get; set; }
        [Column("MPINSalt")]
        [StringLength(500)]
        public string Mpinsalt { get; set; }
        public string DeviceToken { get; set; }
        [Column("TXNPINHash")]
        public string Txnpinhash { get; set; }
        [Column("TXNPINSalt")]
        [StringLength(500)]
        public string Txnpinsalt { get; set; }
        [StringLength(20)]
        public string Status { get; set; }
        [StringLength(1)]
        public string ForcePasswordChange { get; set; }
        [Column("ForceMPINChange")]
        [StringLength(1)]
        public string ForceMpinchange { get; set; }
        [Column("ForceTXNPINChange")]
        [StringLength(1)]
        public string ForceTxnpinchange { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LastLoginLocalDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LastLoginUtcDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LastPasswordChangedLocalDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LastPasswordChangedUtcDate { get; set; }
        [Column("LastMPINChangedLocalDate", TypeName = "datetime")]
        public DateTime? LastMpinchangedLocalDate { get; set; }
        [Column("LastMPINChangedUtcDate", TypeName = "datetime")]
        public DateTime? LastMpinchangedUtcDate { get; set; }
        [Column("LastTXNPINChangedLocalDate", TypeName = "datetime")]
        public DateTime? LastTxnpinchangedLocalDate { get; set; }
        [Column("LastTXNPINChangedUtcDate", TypeName = "datetime")]
        public DateTime? LastTxnpinchangedUtcDate { get; set; }
        public int? FailedLoginAttempt { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? TemporaryLockedTillUtcDate { get; set; }
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
        [StringLength(500)]
        public string ProfileImagePath { get; set; }
    }
}
