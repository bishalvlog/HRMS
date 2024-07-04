using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace HRMS.Data.Comman.Helpers
{
    public class DbConnectionManager
    {
        public static IDbConnection ConnectDb()
        {
            IDbConnection con = new SqlConnection(GetConnectionStrings());
            return con;
        }
        public static string GetConnectionName()
        {
            return "HRMS";
        }
        public static string GetConnectionStrings()
        {
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
            IConfiguration config = builder.Build();
            return config["ConnectionStrings:HRMS"];

        }

        public static void SaveLogErrors(string UserName, string ModuleName, string ControllerName,string ActionName, string ProcedureName, string ErrorMessage)
        {
            using (IDbConnection con = DbConnectionManager.ConnectDb())
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@UserName", UserName);
                param.Add("@ModuleName", ModuleName);
                param.Add("@ControllerName", ControllerName);
                param.Add("@ActionName", ActionName);
                param.Add("@ProcedureName", ProcedureName);
                param.Add("@ErrorMessage", ErrorMessage);
                con.Execute("[dbo].[sp_LogErrors]", param, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
