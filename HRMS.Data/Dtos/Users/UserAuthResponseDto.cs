using HRMS.Data.Dtos.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Data.Dtos.Users
{
    public class UserAuthResponseDto : ApiResponseDto
    {
        public string UserName { get; set; }    
        public string AccessToken { get; set; } 
        public string TokenType { get; set; }   
        public string RefreshToken { get; set; }    
        public long ExpiresIn { get; set; } 


    }
}
