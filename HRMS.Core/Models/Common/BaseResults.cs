using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Core.Models.Common
{
    public class BaseResults
    {
        public BaseResults() 
        { 
            Errors = new List<string>();    
        }
        public bool Success => !Errors.Any();

        public void AddError (string error)
        {
            Errors.Add(error);
        }
        public IList<string> Errors { get; set; }   
    }
}
