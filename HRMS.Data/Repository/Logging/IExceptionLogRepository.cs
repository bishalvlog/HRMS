using HRMS.Core.Models.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Data.Repository.Logging
{
    public interface IExceptionLogRepository
    {
        Task AddAsync (AppExceptionLogParam exception);
    }
}
