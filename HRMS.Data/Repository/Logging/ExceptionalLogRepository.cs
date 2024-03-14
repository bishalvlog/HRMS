using Dapper;
using HRMS.Core.Models.Logger;
using HRMS.Data.Comman.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Data.Repository.Logging
{
    public class ExceptionalLogRepository : IExceptionLogRepository
    {

        public Task AddAsync(AppExceptionLogParam exception)
        {
            using var connection = DbConnectionManager.ConnectDb();

            var param = new DynamicParameters();
            param.Add("", exception.UserName);

            return null;
        }
    }
}
