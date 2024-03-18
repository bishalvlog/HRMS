using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Data.Dtos.UserDto
{
    public class UpdateUserDto
    {
        [Required(ErrorMessage = "Id is required!")]
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public string Department { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public DateTime? DateOfJoining { get; set; }
        public IFormFile ProfileImage { get; set; }
        public bool IsActive { get; set; }
    }
}
