using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalGold.Core.Configuration
{
    public class AutoRateUpdateTime
    {
        public const string Name = "AutoRateUpdateTime";
        public static string SectionName => $"HostedService:{Name}";
        [Range(0, 59)]
        public int AddMinutes { get ; set; }
        [Range(0, 23)]
        public int AddHours { get; set; }
		
    }
}
