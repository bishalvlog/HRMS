using Dapper;
using HRMS.Core.Models.SProc;
using HRMS.Core.Models.Users;
using HRMS.Data.Comman.Helpers;
using System.Data;

namespace HRMS.Data.Repository.Users
{
    public class RolesRepository : IRolesRepository
    {
        public async Task<(SpBaseMessageResponse, AppRole)> CreateRolesAsync(AppRole appRole)
        {
            try
            {
                using var connection = DbConnectionManager.ConnectDb();

                const string OperationMode = "A";

                var param = new DynamicParameters();
                param.Add("@RolesName", appRole.RolesName);
                param.Add("@Description", appRole.Description);
                param.Add("@IsActive", appRole.IsActive);
                param.Add("@LoggedInUser", appRole.CreatedBy);
                param.Add("@OperationMode", OperationMode);

                param.Add("@StatusCode", dbType: DbType.Int32, direction: ParameterDirection.InputOutput);
                param.Add("@MsgType", dbType: DbType.String, size: 10, direction: ParameterDirection.InputOutput);
                param.Add("@MsgText", dbType: DbType.String, size: 100, direction: ParameterDirection.InputOutput);

                var rolesCreate = await connection
                    .QueryFirstOrDefaultAsync<AppRole>("[dbo].[sp_Roles_AddUpdate]",param, commandType: CommandType.StoredProcedure);
                var statusCode = param.Get<int>("@StatusCode");
                var msgType = param.Get<string>("@MsgType");
                var msgText = param.Get<string>("MsgText");

                var spMsgRes = new SpBaseMessageResponse { StatusCode = statusCode, MsgText = msgText, MsgTypes = msgType };

                return (spMsgRes,rolesCreate);

            }
            catch (Exception ex)
            {
                throw ex;

            }
          
        }

        public async Task<(SpBaseMessageResponse, IEnumerable<AppRole>)> GetRolesAsync()
        {
            using var connection = DbConnectionManager.ConnectDb();

            var param = new DynamicParameters();

            param.Add("@StatusCode",dbType: DbType.Int32, direction:ParameterDirection.InputOutput);
            param.Add("@MsgType",dbType: DbType.String, size:10 , direction:ParameterDirection.InputOutput);
            param.Add("@MsgText",dbType: DbType.String, size:100 , direction:ParameterDirection.InputOutput);

            var roles = await connection
                .QueryAsync<AppRole>("[dbo].[sp_Roles_Get_All]",param, commandType: CommandType.StoredProcedure);

            var statusCode = param.Get<int>("@StatusCode");
            var msgType = param.Get<string>("@MsgType");
            var msgText = param.Get<string>("@MsgText");

            var spMsgRes = new SpBaseMessageResponse { StatusCode=statusCode, MsgText =msgText,MsgTypes =msgType };

            return (spMsgRes,roles);

        }

        public async Task<(SpBaseMessageResponse, AppRole)> GetRolesByIdAsync(int id)
        {
            using var connection = DbConnectionManager.ConnectDb();
            var param = new DynamicParameters();
            param.Add("@id", id);
            param.Add("@StatusCode", dbType: DbType.Int32, direction: ParameterDirection.InputOutput);
            param.Add("@MsgType", dbType:DbType.String,size:10, direction:ParameterDirection.InputOutput);
            param.Add("@MsgText",dbType:DbType.String,size :100, direction:ParameterDirection.InputOutput);

            var roles = await connection
                .QueryFirstOrDefaultAsync<AppRole>("[dbo].[sp_role_getby_id]",param,commandType: CommandType.StoredProcedure);
            var statusCode = param.Get<int>("@StatusCode");
            var msgType = param.Get<string>("@MsgType");
            var msgText = param.Get<string>("@MsgText"); 
            
            var spMsgRes = new SpBaseMessageResponse { StatusCode = statusCode, MsgText =msgText, MsgTypes =msgType };

            return (spMsgRes,  roles);  
            
        }

        public async Task<(SpBaseMessageResponse, AppRole)> GetRolesByNameAsync(string roleName)
        {
           if(string.IsNullOrWhiteSpace(roleName))
                throw new ArgumentNullException(nameof(roleName));

           using var connection = DbConnectionManager.ConnectDb();
            var param = new DynamicParameters();
            param.Add("@RolesName", roleName);

            param.Add("@StatusCode", dbType: DbType.Int32, direction:ParameterDirection.InputOutput);    
            param.Add("@MsgType", dbType: DbType.String,size:10, direction:ParameterDirection.InputOutput);    
            param.Add("@MsgText", dbType: DbType.String,size:100, direction:ParameterDirection.InputOutput);
            
            var roles = await connection
                .QueryFirstOrDefaultAsync<AppRole>("sp_Role_Getby_Name",param,commandType: CommandType.StoredProcedure);

            var statusCode = param.Get<int>("@StatusCode");
            var msgType = param.Get<string>("@MsgType");
            var msgText = param.Get<string>("@MsgText");

            var spMsgRes = new SpBaseMessageResponse { StatusCode = statusCode, MsgText =msgText, MsgTypes = msgText };
            return (spMsgRes,roles);
        }

        public async Task<(SpBaseMessageResponse, AppRole)> UpdateRolesAsync(AppRole role)
        {
            using var connection = DbConnectionManager.ConnectDb();

            const string OperationMode = "U";

            var param = new DynamicParameters();
            param.Add("@Id", role.Id);
            param.Add("@RoleName", role.RolesName);
            param.Add("@Description", role.Description);
            param.Add("@IsActive", role.IsActive);
            param.Add("@LoggedInUser", role.CreatedBy);
            param.Add("@OperationMode", OperationMode);

            param.Add("@StatusCode", dbType: DbType.Int32, direction: ParameterDirection.Output);
            param.Add("@MsgType", dbType: DbType.String, size: 10, direction: ParameterDirection.Output);
            param.Add("@MsgText", dbType: DbType.String, size: 200, direction: ParameterDirection.Output);

            var Roleupdate = await connection
                .QueryFirstOrDefaultAsync<AppRole>("[dbo].[sp_Roles_AddUpdate]", param, commandType: CommandType.StoredProcedure);

            var statusCode = param.Get<int>("@StatusCode");
            var MsgType = param.Get<string>("@MsgType");
            var MsgText = param.Get<string>("@MsgText");

            var spMessage = new SpBaseMessageResponse { MsgText = MsgText, StatusCode = statusCode, MsgTypes = MsgText };
            return (spMessage,Roleupdate);

        }
    }
}
