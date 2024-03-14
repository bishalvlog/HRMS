using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Core.Models.Identity
{
    public class AppDbUser : IdentityUser
    {

        [StringLength(200)]
        public string FullName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        [StringLength(20)]
        public string Gender { get; set; }
        public DateTime? DateOfJoining { get; set; }
        [StringLength(500)]
        public string Address { get; set; }
        [StringLength(200)]
        public string Department { get; set; }
        [StringLength(20)]
        public string MobileNumber { get; set; }
        public bool? IsActive { get; set; }
        [StringLength(500)]
        public string ProfileImgagePath { get; set; }
        public DateTime? CreatedLocalDate { get; set; }
        public DateTime? CreatedUTCDate { get; set; }
        [StringLength(10)]
        public string CreatedNepaliDate { get; set; }
        [StringLength(100)]
        public string CreatedBy { get; set; }
        public DateTime? UpdatedLocalDate { get; set; }
        public DateTime? UpdatedUTCDate { get; set; }
        [StringLength(10)]
        public string UpdatedNepaliDate { get; set; }
        [StringLength(100)]
        public string UpdatedBy { get; set; }
        public bool? IsCustomer { get; set; }
    }
}
