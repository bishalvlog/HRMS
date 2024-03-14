using HRMS.Services.Token;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Services.Commond.Extension
{
    public static class HttpContextExtensions
    {
        public static async Task<(bool isForm, string body)> GetRequestBodyAsString(this HttpContext context)
        {
            if (context is null)
                throw new ArgumentNullException(nameof(context));

            if (!context.Request.Body.CanRead)
                return (false, null);

            context.Request.EnableBuffering();

            string requestBody;
            if (context.Request.HasFormContentType)
            {
                requestBody = GetRequestBodyFormData(context);
                return (true, requestBody);
            }

            var buffer = new byte[Convert.ToInt32(context.Request.ContentLength)];
            _ = await context.Request.Body.ReadAsync(buffer.AsMemory(0, buffer.Length));
            requestBody = Encoding.UTF8.GetString(buffer);
            context.Request.Body.Position = 0;

            return (false, requestBody);
        }
        public static string GetIpAddress( this HttpContext context)
        {
            return context.Connection.RemoteIpAddress?.ToString();
        }
        public static string GetRequestUrl (this HttpContext content)
        {
            return $"{content.Request.Scheme}://{content.Request.Host}{content.Request.Path}{content.Request.QueryString}";

        }
        public static string GetQueryString ( this HttpContext content)
        {
            return content.Request.QueryString.HasValue? content.Request.QueryString.Value:string .Empty;
        }
        public static string GetUserAgent (this HttpContext content )
        {
            content.Request.Headers.TryGetValue("User-Agent", out var userAgent);
            return userAgent;
        }
        public static string GetRequestHeaders( this HttpContext content )
        {
            var headers = new List<string>();
            foreach (var header in content.Request.Headers)
                headers.Add($"{header.Key}: {header.Value}");
            return string.Join(";", headers);

        }
        public static string GetController ( this HttpContext content )
        {
            return content.Request.RouteValues["controller"]?.ToString();
        }
        public static string GetAction ( this HttpContext content )
        {
            return content.Request.RouteValues["action"]?.ToString();
        }
        public static string GetUserName ( this HttpContext content ) 
        {
            return content.User?.FindFirst(
                c => c.Type == CustomerClaimTypes.CustomerId || c.Type == UserClaimTypes.UserName)?.Value ?? content.User?.Identity?.Name;
        }
        public static string GetUserEmail(this HttpContext context)
        {
            return context.User?.FindFirst(c => c.Type == CustomerClaimTypes.Email || c.Type == UserClaimTypes.Email)?.Value;
        }
        public static bool IsCustomer(this HttpContext context)
        {
            return context.User?.HasClaim(CustomerClaimTypes.IsCustomer, true.ToString()) ?? false;
        }

        public static async Task<(bool isForm, string body)> GetRequestBodyString ( this HttpContext content )
        {
            if(content is null)
                throw new ArgumentNullException(nameof(content));

            if (!content.Request.Body.CanRead)
                return (false, null);
            content.Request.EnableBuffering();

            string RequestBody;
            if (content.Request.HasFormContentType)
            {
                RequestBody = GetRequestBodyFormData(content);
                return (true, RequestBody);
            }
            var buffer = new byte[Convert.ToInt32(content.Request.ContentLength)];
            _ = await content.Request.Body.ReadAsync(buffer.AsMemory(0, buffer.Length));
            RequestBody = Encoding.UTF8.GetString(buffer);
            content.Request.Body.Position = 0;

            return (true, RequestBody); 

        }
        private static string GetRequestBodyFormData ( this HttpContext content ) 
        {
            var form = content.Request.Form;
            var formdata = new List<string>();
            foreach(var key in form.Keys)
            {
                if (form[key].Count> 1)
                {
                    foreach(var values in form[key])
                    {
                        if (!string.IsNullOrEmpty(values) && !IsFile(values))
                            formdata.Add($"{key} ={values}");
                    }
                }
            }
            return string.Join("&", formdata);
        }

        public static bool IsFile (string values)
        {
            if (string.IsNullOrEmpty(values))
            {
                return false;
            }
            if(values.StartsWith("filename=", StringComparison.OrdinalIgnoreCase) || values.StartsWith("content-type",StringComparison.OrdinalIgnoreCase))
             return true;   
            return false;
        }


    }
}
