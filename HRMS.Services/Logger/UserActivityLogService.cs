using HRMS.Core.Interfaces.Logging;
using HRMS.Core.Models.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Services.Logger
{
    public class UserActivityLogService : IUserActivityLogService
    {
        private readonly IUserActivityLogRepository _activityLogRepository;

        public UserActivityLogService(IUserActivityLogRepository activityLogRepository)
        {
            _activityLogRepository = activityLogRepository;
        }

        public Task LogAsync(UserActivitylogParam userlog)
        {
           return _activityLogRepository.AddAsync(userlog);
        }
    }
}
