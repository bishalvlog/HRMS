﻿using HRMS.Core.Models.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Services.Logger
{
    public interface  IExceptionLogService
    {
        Task LogAsyn(AppExceptionLogParam appExceptionLogParam);
    }
}
