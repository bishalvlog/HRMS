using HRMS.Core.Models.SProc;
using HRMS.Data.Dtos.Response;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Net;
using System.Text;

namespace HRMS.Services.Commond
{
    public abstract class BaseService
    {
        protected (HttpStatusCode, ApiResponseDto) GetErrorResponseFromSprocMessage(SpBaseMessageResponse spMessage)
        {
            return spMessage.StatusCode switch
            {
                400 => (HttpStatusCode.BadRequest, new ApiResponseDto { Success = false, Message = "Bad Request!", Errors = new List<string> { spMessage.MsgText }}),
                401 => (HttpStatusCode.Unauthorized, new ApiResponseDto { Success = false, Message = "Unauthorize User", Errors = new List<string> { spMessage.MsgText}}),
                403 => (HttpStatusCode.Forbidden, new ApiResponseDto { Success = false, Message = "Forbidden!", Errors = new List<string> { spMessage.MsgText}}),
                404 => (HttpStatusCode.NotFound, new ApiResponseDto { Success = false, Message = "Not Found Error!", Errors = new List<string> { spMessage.MsgText} }),
                409 => (HttpStatusCode.Conflict, new ApiResponseDto { Success = false ,Message = "Conflict!", Errors = new List<string> { spMessage.MsgText } }),
                _ => (HttpStatusCode.BadRequest, new ApiResponseDto { Success = false, Message = "Bad Request!",Errors = new List<string> { spMessage.MsgText} })

            };

        }
        protected virtual StringContent GetJsonStringContent(object inputDataObj)
        {
            var jsonSerializerSettings = new JsonSerializerSettings
            {
                ContractResolver =  new DefaultContractResolver { NamingStrategy = new CamelCaseNamingStrategy() },
                Formatting = Formatting.Indented,
            };
            return new StringContent(JsonConvert.SerializeObject(inputDataObj, jsonSerializerSettings), Encoding.UTF8, "application/json");
        }
    }
}
