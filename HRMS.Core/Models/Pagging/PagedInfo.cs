using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Core.Models.Pagging
{
    public class PagedInfo
    {
        public int PageNumber {  get; set; }    
        public int PagedSize {  get; set; }
        public int FilteredCount { get; set; }  
        public int TotalCount { get; set; } 
        public int TotalPages { get; set; }

    }
}
