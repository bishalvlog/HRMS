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
                row["UpdatedLocalDate"] = rmpType.UpdatedLocalDate!;
                row["UpdatedUTCDate"] = rmpType.UpdatedUTCDate!;
                row["UpdatedNepaliDate"] = rmpType.UpdatedNepaliDate;
                row["UpdatedBy"] = rmpType.UpdatedBy;
                dataTableRmp.Rows.Add(row);
            }
            var param = new DynamicParameters();
            param.Add("@RoleId", roleId);
            param.Add("@RoleMenuPermission", dataTableRmp.AsTableValuedParameter("[dbo].[RoleMenuPermissionsType]"));
            param.Add("@sqlActionStatus", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);
            await connection.ExecuteAsync("[dbo].[sp_RoleMenuPermission_GetBy_Id]", param, commandType: CommandType.StoredProcedure);
            var sqlActionsStatus = param.Get<int>("@sqlActionStatus");
            return sqlActionsStatus;

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
    }
}
