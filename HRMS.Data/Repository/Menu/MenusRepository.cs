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
        public async Task<int> AddAsync(MenuAddUpdate menuAddUpdate)
        {
            try
            {
                using var connection = DbConnectionManager.ConnectDb();

                var param = new DynamicParameters();
                param.Add("@Id", menuAddUpdate.Id);
                param.Add("@Title", menuAddUpdate.Title);
                param.Add("@ParentId", menuAddUpdate.ParentId);
                param.Add("@MenuUrl", menuAddUpdate.MenuUrl);
                param.Add("@IsActive", menuAddUpdate.IsActive);
                param.Add("@DisplayOrder", menuAddUpdate.DisplayOrder);
                param.Add("@ImagePath", menuAddUpdate.ImagePath);
                param.Add("@CreatedBy", menuAddUpdate.CreatedBy);
                param.Add("@sqlActionStatus", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

                await connection
                   .ExecuteAsync("[dbo].[sp_menu_Insert]", param, commandType: CommandType.StoredProcedure);
                return param.Get<int>("@sqlActionStatus");
            }
            catch (Exception ex) 
            {

                throw ex;
            }
           


        }

        public async Task<IEnumerable<Menus>> GetListAllAsync()
        {
            try
            {
                using var connection = DbConnectionManager.ConnectDb();

                return await connection
                    .QueryAsync<Menus>("[dbo].[sp_Menu_get_all]", commandType: CommandType.StoredProcedure);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }
    }
}
