using HRMS.Data.Dtos.Response;
using HRMS.Services.Commond.Helpers;
using HRMS.Services.Logger;
using Microsoft.IdentityModel.Logging;
using Newtonsoft.Json.Serialization;
using System.Net;
using System.Text.Json;

namespace HRMS.Middleware
{
    public class ExceptionMiddleWare
    {
        public readonly RequestDelegate _requestDelegate;
        public readonly IHostEnvironment _env;
        public ExceptionMiddleWare(RequestDelegate requestDelegate, IHostEnvironment env)
        {
            _requestDelegate = requestDelegate; 
            _env = env;
        }
        public async Task InvokeAsync(HttpContext content, IExceptionLogService exceptionLogService)
        {
            try
            {
                 await _requestDelegate(content);

            }
            catch (Exception ex)
            {
                if(!content.Request.Path.StartsWithSegments("/Swagger"))
                {
                    try
                    {
                        var appException = await LogHelpers.GetExceptionLogAsync(ex,content,_env);
                        await exceptionLogService.LogAsyn(appException);
                    }
                    catch (Exception)
                    {

                    }


                }
                var statusCode = content.Response.StatusCode;
                var isClientError = statusCode is >= 400 and < 500;

                content.Response.ContentType = "application/json";
                content.Response.StatusCode = isClientError ? statusCode : (int)HttpStatusCode.InternalServerError;

                var response = _env.IsDevelopment() ? isClientError
                    ? new ApiErrorResponseDto(statusCode) :
                    new ApiExceptionDetailsResponseDto((int)HttpStatusCode.InternalServerError, message: ex.Message, details: ex.StackTrace)
                    : isClientError ?
                     new ApiErrorResponseDto(statusCode) : new ApiErrorResponseDto((int)HttpStatusCode.InternalServerError);

                var jsonSerializerOption = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

                var jsonResponse = JsonSerializer.Serialize(response, response.GetType(),jsonSerializerOption);
                await content.Response.WriteAsync(jsonResponse);
            }
        }
    }
}
