using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Data.Dtos.Response
{
    public class ApiErrorResponseDto : ApiResponseDto
    {

        public ApiErrorResponseDto(int statusCode, string message = null, List<string> errors = null)
        {
            Success = false;
            Message = message ?? GetDefaultMessageForStatusCode(statusCode);
            Errors = errors ?? new List<string> { GetDefaultMessageForStatusCode(statusCode) };
        }

        private string GetDefaultMessageForStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "A bad request, you have made",
                401 => "Unauthorized, you are not authorized to access the resource",
                402 => "Payment Required",
                403 => "Forbidden, you don't have permission to access this resource",
                404 => "Resource not found",
                405 => "Method Not Allowed",
                406 => "Not Acceptable",
                407 => "Proxy Authentication Required (RFC 7235)",
                408 => "Request Timeout",
                409 => "A conflict request, resource already exists",
                410 => "Gone, the resource requested is no longer available",
                415 => "Unsupported Media Type",
                422 => "Unprocessable Entity",
                500 => "Internal Server Error, an error occurred while processing your request",
                _ => null
            };
        }
    }
}

