using Dapper;
using HRMS.Core.Dtos.Users;
using HRMS.Core.Interfaces.Users;
using HRMS.Core.Models.Pagging;
using HRMS.Core.Models.SProc;
using HRMS.Core.Models.Users;
using HRMS.Data.Comman.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Data.Repository.Users
{
    public class UserRepository : IUserRepository
    {
        public async Task<(SpBaseMessageResponse, bool)> CheckUserExistsAsync(AppUser appUser)
        {
            try
            {
                using var connection = DbConnectionManager.ConnectDb();

                var param = new DynamicParameters();

                param.Add("@UserName",appUser.UserName);
                param.Add("@Email", appUser.Email);
                param.Add("@Mobile", appUser.Mobile);
                param.Add("@StatusCode", dbType: DbType.Int32, direction: ParameterDirection.Output);
                param.Add("@MsgType", dbType: DbType.String,size:10, direction: ParameterDirection.Output);
                param.Add("@MsgText", dbType: DbType.String,size:100, direction: ParameterDirection.Output);

                var _ = await connection.
                    QueryFirstOrDefaultAsync<AppUser>("[dbo].[sp_check_userExists]",param,commandType:CommandType.StoredProcedure);

                var statusCode = param.Get<int>("@StatusCode");
                var msgType = param.Get<string>("@MsgType");
                var msgText = param.Get<string>("@MsgText");

                var spBaseMsg = new SpBaseMessageResponse { StatusCode =  statusCode , MsgText = msgText, MsgTypes = msgType};

                return (spBaseMsg,statusCode == 409 );

            }catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<(SpBaseMessageResponse, AppUser)> CreateUserAsync(AppUser appUser)
        {
            try
            {
                using var connection = DbConnectionManager.ConnectDb();

                const string OperationMode = "A";
                var param = new DynamicParameters();

                param.Add("@UserName", appUser.UserName);
                param.Add("@FullName", appUser.FullName);
                param.Add("@Email", appUser.Email);
                param.Add("@Mobile", appUser.Mobile);
                param.Add("@Address", appUser.Address);
                param.Add("@Gender", appUser.Gender);
                param.Add("@Department", appUser.Department);
                param.Add("@DateOfBirth", appUser.DateOfBirth);
                param.Add("@DateOfJoining", appUser.DateOfJoining);
                param.Add("@ProfileImagePath", appUser.ProfileImagePath);
                param.Add("@AccessCode", appUser.AccessCode);
                param.Add("@PasswordHash", appUser.PasswordHash);
                param.Add("@PasswordSalt", appUser.PasswordSalt);
                param.Add("@IsActive", appUser.IsActive);
                param.Add("@IsSuperAdmin", appUser.IsSuperAdmin);
                param.Add("@LoggedInUser", appUser.CreatedBy);
                param.Add("@OperationMode", OperationMode);

                param.Add("@StatusCode", dbType: DbType.Int32, direction: ParameterDirection.Output);
                param.Add("@MsgType", dbType: DbType.String, size: 100, direction: ParameterDirection.Output);
                param.Add("@MsgText", dbType: DbType.String, size: 100, direction: ParameterDirection.Output);

                var userCreateDb = await connection
                    .QueryFirstOrDefaultAsync<AppUser>("[dbo].[User_AddUpated]", param, commandType: CommandType.StoredProcedure);

                var statusCode = param.Get<int>("@StatusCode");
                var msgType = param.Get<string>("@MsgType");
                var msgText = param.Get<string>("@MsgText");

                var spMsgRes = new SpBaseMessageResponse { StatusCode = statusCode, MsgText = msgText, MsgTypes = msgType };

                return (spMsgRes, userCreateDb);

            }
            catch (Exception ex)
            {
                throw ex;

            }
            

        }

        public async Task<(SpBaseMessageResponse, AppUser)> GetUserByEmailAsync(string Email)
        {
           using var connection = DbConnectionManager.ConnectDb();

            var param = new DynamicParameters();

            param.Add("@Email", Email);
            param.Add("@Statuscode", dbType:DbType.Int32, direction: ParameterDirection.Output);
            param.Add("@MsgType", dbType: DbType.String, size: 10, direction: ParameterDirection.Output);
            param.Add("@MsgText", dbType: DbType.String, size: 100, direction: ParameterDirection.Output);

            var userEmail = await connection
                .QueryFirstOrDefaultAsync<AppUser>("[dbo].[sp_User_getByEmail]",param,commandType: CommandType.StoredProcedure);
            var statusCode = param.Get<int>("@StatusCode");
            var msgText = param.Get<string>("@MsgText");
            var msgType = param.Get<string>("@MsgType");

            var spMessage = new SpBaseMessageResponse { StatusCode = statusCode, MsgText = msgText, MsgTypes=msgType };
            return (spMessage, userEmail);  

        }

        public async Task<(SpBaseMessageResponse, AppUser)> GetUserByIdAsync(int id)
        {
            using var connection = DbConnectionManager.ConnectDb();

            var param = new DynamicParameters(); 

            param.Add("@Id", id);
            param.Add("@StatusCode", dbType:DbType.Int32, direction: ParameterDirection.Output);
            param.Add("@MsgType",dbType:DbType.String, size: 10, direction: ParameterDirection.Output);
            param.Add("@MsgText", dbType:DbType.String,size:100,direction: ParameterDirection.Output);

            var user = await connection
                .QueryFirstOrDefaultAsync<AppUser>("[dbo].[sp_User_GetBy_Id]",param,commandType: CommandType.StoredProcedure);
            var statusCode = param.Get<int>("@StatusCode");
            var msgText = param.Get<string>("@MsgText");
            var msgtype = param.Get<string>("@MsgType");

            var spMessage = new SpBaseMessageResponse { StatusCode = statusCode, MsgText = msgText, MsgTypes = msgtype };
            return (spMessage,user);
        }

        public async Task<(SpBaseMessageResponse, AppUser)> GetUserByUserNameAsync(string UserName)
        {
           using var connection = DbConnectionManager.ConnectDb();

            var param = new DynamicParameters();

            param.Add("@UserName", UserName);

            param.Add("@StatusCode", dbType: DbType.Int32, direction: ParameterDirection.Output);
            param.Add("@MsgType", dbType: DbType.String, size: 10, direction:ParameterDirection.Output);
            param.Add("@MsgText",dbType:DbType.String,size:100, direction:ParameterDirection.Output);

            var user = await connection
                .QueryFirstOrDefaultAsync<AppUser>("[dbo].[Sp_User_GetBy_UserName]", param, commandType: CommandType.StoredProcedure);
            var statusCode = param.Get<int>("@StatusCode");
            var msgText = param.Get<string>("@MsgText");
            var msgType = param.Get<string>("@MsgType");

            var spMsg = new SpBaseMessageResponse { StatusCode = statusCode, MsgText=msgText, MsgTypes = msgType };

            return (spMsg, user);
        }

        public Task<PageResponse<AppUser>> GetUsersAsync(UserListRequest userListRequest)
        {
            
        }

        public async Task<(SpBaseMessageResponse, AppUser)> UpdateUserAsync(AppUser appUser)
        {
            try
            {
                using var connection = DbConnectionManager.ConnectDb();

                const string OperationMode = "U";
                var param = new DynamicParameters();

                param.Add("@UserName", appUser.UserName);
                param.Add("@FullName", appUser.FullName);
                param.Add("@Email", appUser.Email);
                param.Add("@Mobile", appUser.Mobile);
                param.Add("@Address", appUser.Address);
                param.Add("@Gender", appUser.Gender);
                param.Add("@Department", appUser.Department);
                param.Add("@DateOfBirth", appUser.DateOfBirth);
                param.Add("@DateOfJoining", appUser.DateOfJoining);
                param.Add("@ProfileImagePath", appUser.ProfileImagePath);
                param.Add("@AccessCode", appUser.AccessCode);
                param.Add("@PasswordHash", appUser.PasswordHash);
                param.Add("@PasswordSalt", appUser.PasswordSalt);
                param.Add("@IsActive", appUser.IsActive);
                param.Add("@IsSuperAdmin", appUser.IsSuperAdmin);
                param.Add("@LoggedInUser", appUser.CreatedBy);
                param.Add("@OperationMode", OperationMode);

                param.Add("@StatusCode", dbType: DbType.Int32, direction: ParameterDirection.Output);
                param.Add("@MsgType", dbType: DbType.String, size: 100, direction: ParameterDirection.Output);
                param.Add("@MsgText", dbType: DbType.String, size: 100, direction: ParameterDirection.Output);

                var userCreateDb = await connection
                    .QueryFirstOrDefaultAsync<AppUser>("[dbo].[User_AddUpated]", param, commandType: CommandType.StoredProcedure);

                var statusCode = param.Get<int>("@StatusCode");
                var msgType = param.Get<string>("@MsgType");
                var msgText = param.Get<string>("@MsgText");

                var spMsgRes = new SpBaseMessageResponse { StatusCode = statusCode, MsgText = msgText, MsgTypes = msgType };

                return (spMsgRes, userCreateDb);

            }
            catch (Exception ex)
            {
                throw ex;

            }
        }
    }
}
