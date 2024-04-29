using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Data.Dtos.Auth
{
    public class BaseAuthRequestDto
    {
        public string GrantType { get; set; }   
        public string Source { get; set; }  
    }
}
