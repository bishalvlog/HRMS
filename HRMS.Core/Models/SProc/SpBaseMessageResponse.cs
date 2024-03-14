using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Core.Models.SProc
{
    public class SpBaseMessageResponse
    {
        public int StatusCode { get; set; } 
        public string MsgTypes { get; set; }    
        public string MsgText { get; set; } 
        public int? ReturnPrimaryId { get; set; }
    }
}
