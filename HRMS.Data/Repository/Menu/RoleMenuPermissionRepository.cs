using AutoMapper;
using Dapper;
using HRMS.Core.Interfaces.Menu;
using HRMS.Core.Models.Menu;
using HRMS.Data.Comman.Helpers;
using HRMS.Data.Repository.Menu.Types;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Data.Repository.Menu
{
    public class RoleMenuPermissionRepository : IRoleMenuPermissionRepository
    {
        private readonly IMapper _mapper;
        public RoleMenuPermissionRepository(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<int> AddListAsync(int roleId, IEnumerable<RoleMenuPermissionTypes> listRoleMenuPermissionsType)
        {
             try
             {
                    using var connection = DbConnectionManager.ConnectDb();

                    var dataTableRmp = GetDataTableRoleMenuPermissions();

                    foreach (var rmpType in listRoleMenuPermissionsType)
                    {
                        var row = dataTableRmp.NewRow();
                        row["MenuId"] = rmpType.MenuId;
                        row["ViewPer"] = rmpType.ViewPer;
                        row["CreatePer"] = rmpType.CreatePer;
                        row["UpdatePer"] = rmpType.UpdatePer;
                        row["DeletePer"] = rmpType.DeletePer;
                    row["UpdatedLocalDate"] = DBNull.Value;
                       row["UpdatedUTCDate"] = DBNull.Value;
                        row["UpdatedNepaliDate"] = rmpType?.UpdatedNepaliDate;
                        row["UpdatedBy"] = rmpType.UpdatedBy;
                        dataTableRmp.Rows.Add(row);
                    }
                    var param = new DynamicParameters();
                    param.Add("@RoleId", roleId);
                    param.Add("@RoleMenuPermissions", dataTableRmp.AsTableValuedParameter("[dbo].[RoleMenuPermissionsType]"));
                    param.Add("@sqlActionStatus", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);
                    await connection.ExecuteAsync("[dbo].[sp_rolemenupermissions_upsert_byroleid]", param, commandType: CommandType.StoredProcedure);
                    var sqlActionsStatus = param.Get<int>("@sqlActionStatus");
                    return sqlActionsStatus;
             }
             catch (Exception ex)
             {
                    throw ex;
             }
            

        }
        public async Task<int> UpdateListAsync(int roleId, IEnumerable<RoleMenuPermissionTypes> listRoleMenuPermissionsType)
        {
            using var connection = DbConnectionManager.ConnectDb();
            var dataTableRmp = GetDataTableRoleMenuPermissions();

            foreach(var rmpType in listRoleMenuPermissionsType)
            {
                var row = dataTableRmp.NewRow();
                row["MenuId"] = rmpType.MenuId;
                row["ViewPer"] = rmpType.ViewPer;
                row["CreatePer"] = rmpType.CreatePer;
                row["UpdatePer"] = rmpType.UpdatePer;
                row["DeletePer"] = rmpType.DeletePer;
                row["UpdatedLocalDate"] = rmpType.UpdatedLocalDate;
                row["UpdatedUTCDate"] = rmpType.UpdatedUTCDate;
                row["UpdatedNepaliDate"] = rmpType.UpdatedNepaliDate;
                row["UpdatedBy"] = rmpType.UpdatedBy;
                dataTableRmp.Rows.Add(row);
            }
            var param = new DynamicParameters();
            param.Add("@RoleId", roleId);
            param.Add("@RoleMenuPermission", dataTableRmp.AsTableValuedParameter("[dbo].[RoleMenuPermissionsType]"));
            param.Add("@sqlActionStatus", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);
            await connection.ExecuteAsync("[dbo].[sp_rolemenupermissions_upsert_byroleid]", param, commandType: CommandType.StoredProcedure);
            var sqlActionStatus = param.Get<int>("@sqlActionStatus");
            return sqlActionStatus;
        }


        public async Task<IEnumerable<RoleMenuPermissions>> GetByRoleMenu(int roleId)
        {
           using var connection = DbConnectionManager.ConnectDb();
            var param = new DynamicParameters();

            param.Add("@RoleId", roleId);

            return await connection
                .QueryAsync<RoleMenuPermissions>("[dbo].[sp_RoleMenuPermission_GetBy_Id]", param, commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<RoleMenuPermissions>> GetListAsync()
        {
            using var connection  = DbConnectionManager.ConnectDb();
            var param = new DynamicParameters(); 
            
            return await connection
                .QueryAsync<RoleMenuPermissions>("[dbo].[sp_rolemenupermission_getall]",commandType: CommandType.StoredProcedure);
        }
        private DataTable GetDataTableRoleMenuPermissions()
        {
            var dataTableRmp = new DataTable();
            dataTableRmp.Columns.Add("MenuId", typeof(int));
            dataTableRmp.Columns.Add("ViewPer", typeof(bool));
            dataTableRmp.Columns.Add("CreatePer", typeof(bool));
            dataTableRmp.Columns.Add("UpdatePer", typeof(bool));
            dataTableRmp.Columns.Add("DeletePer", typeof(bool));
            dataTableRmp.Columns.Add("CreatedLocalDate", typeof(DateTime)).AllowDBNull = true;
            dataTableRmp.Columns.Add("CreatedUTCDate", typeof(DateTime)).AllowDBNull = true;
            dataTableRmp.Columns.Add("CreatedNepaliDate", typeof(string)).AllowDBNull = true;
            dataTableRmp.Columns.Add("CreatedBy", typeof(string)).AllowDBNull = true;
            dataTableRmp.Columns.Add("UpdatedLocalDate", typeof(DateTime)).AllowDBNull = true;
            dataTableRmp.Columns.Add("UpdatedUTCDate", typeof(DateTime)).AllowDBNull = true;
            dataTableRmp.Columns.Add("UpdatedNepaliDate", typeof(string)).AllowDBNull = true;
            dataTableRmp.Columns.Add("UpdatedBy", typeof(string)).AllowDBNull = true;
            return dataTableRmp;
        }

        public async Task<IEnumerable<MenuWithSubmenus>> GetListWithSubMenusAsync(string userName)
        {
           using var connection = DbConnectionManager.ConnectDb();

            var param = new DynamicParameters();
            param.Add("@UserName", userName);

            var listOfMenuPermission = await connection
                .QueryAsync<RoleMenuPermissions>("[dbo].[sp_menus_rolePermission_getByUserName]",param,commandType:CommandType.StoredProcedure);
            return GetMenuWithSubMenu(listOfMenuPermission);
        }
        public async Task<MenuWithSubmenus> GetMenuSingleWithSubmenusByIdAsync(int menuId, string userName)
        {
            using var connection = DbConnectionManager.ConnectDb();

            var param = new DynamicParameters();

            var listMenuRoleSubMenus = await connection
                .QueryAsync<RoleMenuPermissions>("[dbo].[sp_menus_rolemenupermissions_get_by_menuid_username]", param, commandType: CommandType.StoredProcedure);
            return GetMenuSingleWithSubmenus(menuId, listMenuRoleSubMenus);
        }
        private IEnumerable<MenuWithSubmenus> GetMenuWithSubMenu(IEnumerable<RoleMenuPermissions> menuIn)
        {
            var menu = menuIn.
                Where(x => x.MenuId !=x.ParentId)
                .Select(s => _mapper.Map<RoleMenuPermissions,MenuWithSubmenus>(s))
                .ToList();

            var menuAggregation = new List<MenuWithSubmenus>();
            menu.ForEach(menuItem =>
                {
                    var menuItemSubMenu = menu.Where(x => x.ParentId == menuItem.MenuId)
                        .OrderBy(x => x.DisplayOrder)
                        .ThenBy(x => x.Title)
                        .ToList();
                    menuItem.SubMenus.AddRange(menuItemSubMenu);
                    menuAggregation.Add(menuItem);
            });
                return menuAggregation
                .Where(x => x.ParentId<1)
                .OrderBy(x => x.DisplayOrder)
                .ThenBy(x => x.Title)
                .ToList();
        }
        private MenuWithSubmenus GetMenuSingleWithSubmenus(int menuId, IEnumerable<RoleMenuPermissions> menusIn)
        {
            var menus = menusIn
                .Where(x => x.MenuId != x.ParentId)
                .Select(s => _mapper.Map<RoleMenuPermissions, MenuWithSubmenus>(s))
                .ToList();

            var menusAggregated = new List<MenuWithSubmenus>();
            menus.ForEach(menuItem =>
            {
                var menuItemSubmenus = menus.Where(x => x.ParentId == menuItem.MenuId)
                    .OrderBy(x => x.DisplayOrder)
                    .ThenBy(x => x.Title)
                    .ToList();

                menuItem.SubMenus.AddRange(menuItemSubmenus);
                menusAggregated.Add(menuItem);
            });

            return menusAggregated.FirstOrDefault(x => x.MenuId == menuId);
        }
    }
}
