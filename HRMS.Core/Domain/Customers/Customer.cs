using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Core.Domain.Customers
{
    public class Customer
    {
        public string CustomerId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string CountryCallingCode { get; set; }
        public string Mobile { get; set; }
        public bool MobileConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public string MPINHash { get; set; }
        public string MPINSalt { get; set; }
        public string DeviceToken { get; set; }
        public string TXNPINHash { get; set; }
        public string TXNPINSalt { get; set; }
        public char Status { get; set; }
        public char ForcePasswordChange { get; set; }
        public char ForceMPINChange { get; set; }
        public char ForceTXNPINChange { get; set; }
        public DateTime? LastLoginLocalDate { get; set; }
        public DateTime? LastLoginUtcDate { get; set; }
        public DateTime? LastPasswordChangedLocalDate { get; set; }
        public DateTime? LastPasswordChangedUtcDate { get; set; }
        public DateTime? LastMPINChangedLocalDate { get; set; }
        public DateTime? LastMPINChangedUtcDate { get; set; }
        public DateTime? LastTXNPINChangedLocalDate { get; set; }
        public DateTime? LastTXNPINChangedUtcDate { get; set; }
        public int FailedLoginAttempt { get; set; }
        public DateTime? TemporaryLockedTillUtcDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedLocalDate { get; set; }
        public DateTime? CreatedUtcDate { get; set; }
        public string CreatedNepaliDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedLocalDate { get; set; }
        public DateTime? UpdatedUtcDate { get; set; }
        public string UpdatedNepaliDate { get; set; }
        public string ProfileImagePath { get; set; }
        public char? KycStatusCode { get; set; }
    }
}
