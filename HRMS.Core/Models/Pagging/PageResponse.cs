using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Core.Models.Pagging
{
    public class PageResponse<T> : PagedInfo
    {

        private IEnumerable<T> _items = new List<T>();
        public IEnumerable<T> Items 
        {
            get => _items;
                set => _items = value ?? new List<T>();

        }
    }
}
