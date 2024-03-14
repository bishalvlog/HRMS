using Org.BouncyCastle.Bcpg.Sig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Data.Dtos.Response
{
    public class ApiResponseDto
    {
        private List<string> _error = new ();
        private string _message = string.Empty;
        public bool Success { get;  set; }  
        public string Message { get => _message; set => _message =value ?? string.Empty; } 

        public object Data { get; set; }    
        public List<string> Errors { get => _error; set => _error = value ?? new(); } 
    }
}
