using Dapper;
using HRMS.Core.Interfaces.Menu;
using HRMS.Core.Models.Menu;
using HRMS.Data.Comman.Helpers;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Data.Repository.Menu
{
    public class MenusRepository : IMenuRepository
    {
        public async Task<IEnumerable<Menus>> GetListAllAsync()
        {
            using var connection = DbConnectionManager.ConnectDb();

            return await connection
                .QueryAsync<Menus>("[dbo].[sp_Menu_get_all]", commandType: CommandType.StoredProcedure);
        }
    }
}
