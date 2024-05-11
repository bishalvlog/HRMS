using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Core.Models.ClientMenu
{
    public class ClientMenuModel
    {
        public int Sectionid { get; set; }
        public string SectionName { get; set; }
        public string ProductCode { get; set; }
        public string ServiceTypeCode { get; set; }
        public string Url { get; set; }
    }
    public class ClientMenuSubsection
    {
        public string SectionName { get; set; }
        public string ProductCode { get; set; }
        public string ServiceTypeCode { get; set; }
        public string Url { get; set; }
    }
}
