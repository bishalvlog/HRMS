using Org.BouncyCastle.Asn1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Core.Models.Auth
{
    public class RefreshToken
    {
        public string CustomerID { get; set; } 
        public string UserName { get; set; }    
        public string Token {  get; set; }
        public string JwtId { get; set; }   
        public bool IsRevoked { get; set; }   
        public DateTime? GeneratedLocalDate { get; set; }   
        public DateTime? GeneratedUtcTime { get; set; }
        public DateTime? ExpiredLocalDate { get; set; }    
        public DateTime? ExpiredUtcTime { get; set; }   
    }
}
