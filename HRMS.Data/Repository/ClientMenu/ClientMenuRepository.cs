using Dapper;
using HRMS.Core.Models.ClientMenu;
using HRMS.Data.Comman.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Data.Repository.ClientMenu
{
    public class ClientMenuRepository : IClientMenuRepository
    {
        public async Task<IEnumerable<ClientMenuModel>> GetListAllAsync(string ProductTypeCode)
        {
           using var connection = DbConnectionManager.ConnectDb();
            var param = new DynamicParameters();

            param.Add("@ProductTypeCode", ProductTypeCode);

            return await connection
                 .QueryAsync<ClientMenuModel>("[dbo].[sp_get_menu_section]", param, commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<ClientMenuSubsection>> GetSubsectionAsync(int ParentSectionId)
        {
            using var connection = DbConnectionManager.ConnectDb();
            var param = new DynamicParameters();
            param.Add("@ParentSectionId", ParentSectionId);

            return await connection
                .QueryAsync<ClientMenuSubsection>("[dbo].[sp_get_menu_subsection]",param, commandType: CommandType.StoredProcedure);

        }
    }
}
