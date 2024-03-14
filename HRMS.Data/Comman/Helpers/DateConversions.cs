using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Data.Comman.Helpers
{
    public static class DateConversions
    {
        public static string ConvertToNepaliDate(DateTime dateEnglish)
        {
            try
            {
                using (IDbConnection conn = DbConnectionManager.ConnectDb())
                {
                    DynamicParameters param = new DynamicParameters();
                    param.Add("@EnglishDate",dateEnglish);
                    string nepalidate = conn.ExecuteScalar<string>("[dbo].[sp_Get_Nepali_date]", param, commandType: CommandType.StoredProcedure);

                    return nepalidate;

                }

            }
            catch (Exception ex)
            {
                return null;
            }

        }

    }
}
