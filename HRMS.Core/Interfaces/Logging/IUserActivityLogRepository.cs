using HRMS.Core.Models.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Core.Interfaces.Logging
{
    public interface IUserActivityLogRepository
    {
        Task AddAsync(UserActivitylogParam activitylogParam);
    }
}
