using Dapper;
using HRMS.Core.Interfaces.Users;
using HRMS.Core.Models.SProc;
using HRMS.Data.Comman.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Data.Repository.Users
{
    public class UserRolesRepository : IUserRolesRepository
    {
        public async Task<SpBaseMessageResponse> AssignUserToRolesAsync(int userId, params int[] roleIds)
        {
           using var connection = DbConnectionManager.ConnectDb();

            var param = new DynamicParameters();
            param.Add("@UserId", userId);
            param.Add("@RolesId",string.Join(',', roleIds));

            param.Add("@StatusCode", dbType: DbType.Int32,direction:ParameterDirection.Output);
            param.Add("@MsgType", dbType: DbType.String, size: 10, direction: ParameterDirection.Output);
            param.Add("@MsgText",dbType:DbType.String,size:100, direction: ParameterDirection.Output);

            var _ = await connection.ExecuteAsync("[dbo].[sp_Assign_User_Roles]", param, commandType: CommandType.StoredProcedure);

            var statusCode = param.Get<int>("@StatusCode");
            var MsgType = param.Get<string>("@MsgType");
            var MsgText = param.Get<string>("@MsgText");

           return new SpBaseMessageResponse { StatusCode = statusCode, MsgText = MsgText, MsgTypes = MsgType };
            
        }
    }
}
