using Dapper;
using HRMS.Core.Interfaces.Logging;
using HRMS.Core.Models.Logger;
using HRMS.Data.Comman.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Data.Logging
{
    public class UserActivityLogRepository : IUserActivityLogRepository
    {
        public async Task AddAsync(UserActivitylogParam activityLogParam)
        {
            using var connection = DbConnectionManager.ConnectDb();

            var param = new DynamicParameters();
            param.Add("@UserName", activityLogParam.UserName);
            param.Add("@Email", activityLogParam.Email);
            param.Add("@IsCustomer", activityLogParam.IsCustomer);
            param.Add("@UserAgent", activityLogParam.UserAgent);
            param.Add("@RemoteIpAddress", activityLogParam.RemoteIpAddress);
            param.Add("@HttpMethod", activityLogParam.HttpMethod);
            param.Add("@ControllerName", activityLogParam.ControllerName);
            param.Add("@ActionName", activityLogParam.ActionName);
            param.Add("@QueryString", activityLogParam.QueryString);
            param.Add("@IsFormData", activityLogParam.IsFormData);
            param.Add("@RequestBody", activityLogParam.RequestBody);
            param.Add("@Headers", activityLogParam.Headers);
            param.Add("@RequestUrl", activityLogParam.RequestUrl);
            param.Add("@MachineName", activityLogParam.MachineName);
            param.Add("@Environment", activityLogParam.Environment);
            param.Add("@UserAction", activityLogParam.UserAction);

            var _ = await connection.ExecuteAsync("[dbo].[sp_log_user_activity]", param, commandType: CommandType.StoredProcedure);
        }
    }
}
