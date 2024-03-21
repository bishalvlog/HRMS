using HRMS.Core.Models.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Core.Interfaces.Menu
{
    public interface IMenuRepository
    {
        Task<IEnumerable<Menus>> GetListAllAsync();
        Task<int> AddAsync(MenuAddUpdate menuAddUpdate);
    }
}
