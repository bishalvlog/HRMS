using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Core.Models.Logger
{
    public class AppExceptionLogParam
    {
        public string UserName { get; set; }    
        public string UserAgent { get; set; }   
        public string RemoteIPAddress { get; set; } 
        public string ControllerName { get; set; }  
        public string ActionName { get; set; }  
        public string QuerString { get; set; }  
        public string FormData { get; set; }    
        public string Headers { get; set; }  
        public string RequestUrl { get; set; }      
        public string HttpMethod { get; set; }  
        public string RequestBody { get; set; } 
        public string ExceptionType { get; set; }   
        public string ExceptionMessage { get; set; }    
        public string ExceptionStackTrace { get; set; } 
        public string InnerExceptionMessage { get; set; }
        public string InnerExceptionStackTrace { get; set; }
        public string MachineName { get; set; }
        public string Enviroment { get; set; }    
    }
}
