using AutoMapper;
using Dapper;
using HRMS.Core.Interfaces.Menu;
using HRMS.Core.Models.Menu;
using HRMS.Data.Comman.Helpers;
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
    }
}
