using HRMS.Core.Models.SProc;
using HRMS.Core.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Core.Interfaces.Users
{
    public interface IUserRolesRepository
    {
        Task<SpBaseMessageResponse> AssignUserToRolesAsync(int userId, params int[] roleIds);
        Task<IEnumerable<AppRole>>GetUserRolesByNameAsync(string userName);
    }
}
