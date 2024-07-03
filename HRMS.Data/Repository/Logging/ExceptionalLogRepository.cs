using Dapper;
using HRMS.Core.Models.Logger;
using HRMS.Data.Comman.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Data.Repository.Logging
{
    public class ExceptionalLogRepository : IExceptionLogRepository
    {

        public async Task AddAsync(AppExceptionLogParam exception)
        {
             try
             {
                using var connection = DbConnectionManager.ConnectDb();

                var param = new DynamicParameters();
                param.Add("@UserName", exception.UserName);
                param.Add("@UserAgent", exception.UserAgent);
                param.Add("@RemoteIpAddress", exception.RemoteIPAddress);
                param.Add("@ControllerName", exception.ControllerName);
                param.Add("@ActionName", exception.ActionName);
                param.Add("@QueryString", exception.QuerString);
                param.Add("@FormDate", exception.FormDate);
                param.Add("@Headers", exception.Headers);
                param.Add("@RequestUrl", exception.RequestUrl);
                param.Add("@HttpMethod", exception.HttpMethod);
                param.Add("@RequestBody", exception.RequestBody);
                param.Add("@ExceptionType", exception.ExceptionType);
                param.Add("@ExceptionMessage", exception.ExceptionMessage);
                param.Add("@ExceptionStackTrace", exception.ExceptionStackTrace);
                param.Add("@InnerExceptionMessage", exception.InnerExceptionMessage);
                param.Add("@MachineName", exception.MachineName);
                param.Add("@Environment", exception.Enviroment);

                var _ = await connection.ExecuteAsync("[dbo].[sp_log_exception]", param, commandType: CommandType.StoredProcedure);

             }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
