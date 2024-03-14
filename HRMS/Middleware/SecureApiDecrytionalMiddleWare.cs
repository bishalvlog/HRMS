using HRMS.Core.Comman;
using HRMS.Services.SecureApi;
using System.Text;

namespace HRMS.Middleware
{
    public class SecureApiDecrytionalMiddleWare
    {
        private readonly RequestDelegate _requestDelegate;
        public SecureApiDecrytionalMiddleWare( RequestDelegate requestDelegate)
        {
            _requestDelegate = requestDelegate;
        }

        public async Task InvokeAsync (HttpContext httpContext, IConfiguration config , ISecureApiCrytoService secureApiCrytoService )
        {
            var secureApiPath = config["SecureApi:Path"];
            var isSecureApiPath = httpContext.Request.Path.StartsWithSegments(secureApiPath);
            if (!isSecureApiPath )
            {
                await _requestDelegate(httpContext);
                return;
            }
            var hasRequestBody = httpContext.Request.ContentLength.HasValue && httpContext.Request.ContentLength.Value > 0;
            if(!hasRequestBody )
            {
                await _requestDelegate(httpContext);    
                return; 
            }
            using var reader = new StreamReader(
                httpContext.Request.Body, encoding: Encoding.UTF8, detectEncodingFromByteOrderMarks: false, bufferSize: 1024, leaveOpen :true);    
            var requestBody = await reader.ReadToEndAsync();
            var (keyBytes , ivBytes, cipheredDataBytes) = DgCryptoHelper.GetKeyIvDataBytesFromBase64Encoded(requestBody);
            var plainDataBytes = await secureApiCrytoService.DecryptHybrideAsync(cipheredDataBytes, keyBytes, ivBytes);

            var memeryStream    = new MemoryStream(plainDataBytes);   
            httpContext.Request.Body = memeryStream;
            httpContext.Request.ContentLength = memeryStream.Length;
            httpContext.Request.Body.Position = 0;

            await _requestDelegate(httpContext);

        }
    }
}
