using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Core.Models.Users
{
    public class AppRole
    {
        public int Id { get; set; } 
        public string RolesName { get; set; }   
        public string Description { get; set; } 
        public bool IsActive { get; set; }  
        public string CreatedBy { get; set; }   
        public DateTime? CreatedLocalDate { get; set; } 
        public DateTime? CreatedUtcDate { get; set; }
        public string CreateNepaliDate { get; set; }    
        public string UpdatedBy { get; set; }   
        public DateTime? UpdatedLocalDate { get; set; }
        public DateTime? UpdatedUtcDate { get;set; }
        public string UpdatedNepaliDate { get; set; }   

    }
}
