using HRMS.Core.Models.ClientMenu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Data.Repository.ClientMenu
{
    public interface IClientMenuRepository
    {
        Task<IEnumerable<ClientMenuModel>> GetListAllAsync(string ProductTypeCode);
        Task<IEnumerable<ClientMenuSubsection>> GetSubsectionAsync(int ParentSectionId);
    }
}