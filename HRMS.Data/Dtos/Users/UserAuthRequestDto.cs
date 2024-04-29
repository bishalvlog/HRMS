using HRMS.Data.Dtos.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Data.Dtos.Users
{
    public class UserAuthRequestDto : BaseAuthRequestDto
    {
        public string UserNameOrEmail { get; set; } 
        public string Passord { get; set; }
        public string RefreshToken {  get; set; }     
    }
}
