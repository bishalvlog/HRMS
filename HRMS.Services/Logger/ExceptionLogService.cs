using HRMS.Core.Models.Logger;
using HRMS.Data.Repository.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Services.Logger
{
    public class ExceptionLogService : IExceptionLogService
    {
        private readonly IExceptionLogRepository _exceptionRepository;

        public ExceptionLogService(IExceptionLogRepository exceptionRepository)
        {
            _exceptionRepository = exceptionRepository;
        }
        public async Task LogAsyn(AppExceptionLogParam appExceptionLogParam)
        {
            await _exceptionRepository.AddAsync(appExceptionLogParam);
        }
    }
}
