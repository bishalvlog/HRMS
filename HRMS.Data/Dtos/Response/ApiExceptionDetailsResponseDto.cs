using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Data.Dtos.Response
{
    public class ApiExceptionDetailsResponseDto : ApiErrorResponseDto
    {
        public ApiExceptionDetailsResponseDto(int statusCode, string message = null, string details = null, List<string> errors = null)
           : base(statusCode, message: message, errors: errors)
        {
            Details = details;
        }

        public string Details { get; set; }

    }
}
