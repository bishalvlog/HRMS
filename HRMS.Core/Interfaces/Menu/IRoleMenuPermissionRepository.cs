using HRMS.Core.Models.Menu;
using HRMS.Data.Repository.Menu.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Core.Interfaces.Menu
{
    public interface IRoleMenuPermissionRepository
    {
        Task<IEnumerable<RoleMenuPermissions>> GetByRoleMenu(int roleId);
        Task<IEnumerable<RoleMenuPermissions>> GetListAsync();
        Task<int> AddListAsync(int roleId, IEnumerable<RoleMenuPermissionTypes> listRoleMenuPermissionsType);
    }
}
