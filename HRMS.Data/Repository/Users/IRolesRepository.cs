using HRMS.Core.Models.Pagging;
using HRMS.Core.Models.SProc;
using HRMS.Core.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Data.Repository.Users
{
    public interface IRolesRepository
    {
        Task<(SpBaseMessageResponse, AppRole)> CreateRolesAsync (AppRole appRole);
        Task<(SpBaseMessageResponse,IEnumerable<AppRole>)> GetRolesAsync();
        Task<(SpBaseMessageResponse,AppRole)> GetRolesByIdAsync (int id);
        Task<(SpBaseMessageResponse,AppRole)> GetRolesByNameAsync(string roleName);
        Task<(SpBaseMessageResponse,AppRole)> UpdateRolesAsync(AppRole appRole);
        Task<(SpBaseMessageResponse,AppRole)> DeleteRoleAsync(int id);  
       


    }
}
