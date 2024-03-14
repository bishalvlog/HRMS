using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
