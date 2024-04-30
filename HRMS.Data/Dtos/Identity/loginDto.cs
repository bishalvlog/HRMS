using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Data.Dtos.Identity
{
    public  class loginDto
    {
        [Required (ErrorMessage = "User Name is Required !")]
        public string UserName { get; set; }
        [Required (ErrorMessage = "Password is Required !")]
        public string Password { get; set; }   
    }
    public class UserLoginDto
    {
        [Required(ErrorMessage = "User Name is Required !")]
        public string UserName { get; set; }
        [Required (ErrorMessage = "Password is Required !")]
        public string Password { get; set; }
    }
}
