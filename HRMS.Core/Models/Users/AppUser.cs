using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Core.Models.Users
{
    public class AppUser
    {
        public int Id { get; set; } 
        public string UserName { get; set; }    
        public string FullName { get; set; }    
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; } 
        public int Gender { get; set; } 
        public string GenderName { get; set; }
        public string Department { get; set; }  
        public DateTime? DateOfBirth { get; set; }  
        public DateTime? DateOfJoining { get; set; }    
        public string ProfileImagePath { get; set; }
        public string AccessCode { get; set; }   
        public string PasswordHash { get; set; } 
        public string PasswordSalt { get; set; }
        public bool IsActive { get; set; }
        public bool IsSuperAdmin { get; set; }  
        public DateTime? LastLoginDate {  get; set; }   
        public string CreatedBy { get; set; }   
        public DateTime? CreatedLocalDate { get; set; } 
        public DateTime? CreatedUtcDate { get; set; }
        public string CreatedNepaliDate {  get; set; }  
        public string UpdatedBy { get; set; }   
        public DateTime? UpdatedLocalDate { get; set; } 
        public DateTime? UpdatedLocalTime { get; set; }
        public string UpdatedNepaliDate { get; set; }   

    }
    public class AppUserOutCred
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }
        public int Gender { get; set; }
        public string GenderName { get; set; }
        public string Department { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public DateTime? DateOfJoining { get; set; }
        public string ProfileImagePath { get; set; }
        public string AccessCode { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public bool IsActive { get; set; }
        public bool IsSuperAdmin { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedLocalDate { get; set; }
        public DateTime? CreatedUtcDate { get; set; }
        public string CreatedNepaliDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedLocalDate { get; set; }
        public DateTime? UpdatedLocalTime { get; set; }
        public string UpdatedNepaliDate { get; set; }
    }
}
