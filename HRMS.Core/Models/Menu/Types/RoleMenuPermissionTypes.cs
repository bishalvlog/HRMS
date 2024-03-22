using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Data.Repository.Menu.Types
{
    public class RoleMenuPermissionTypes
    {
        [Required]
        public int MenuId { get; set; }
        [Required]
        public bool ViewPer { get; set; }
        [Required]
        public bool CreatePer { get; set; }
        [Required]
        public bool UpdatePer { get; set; }
        [Required]
        public bool DeletePer { get; set; }
        public DateTime? CreatedLocalDate { get; set; }
        public DateTime? CreatedUTCDate { get; set; }
        public string CreatedNepaliDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedLocalDate { get; set; }
        public DateTime? UpdatedUTCDate { get; set; }
        public string UpdatedNepaliDate { get; set; }
        public string UpdatedBy { get; set; }
    }
}
