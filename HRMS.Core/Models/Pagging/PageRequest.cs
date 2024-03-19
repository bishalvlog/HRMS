using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Core.Models.Pagging
{
    public class PageRequest
    {
        protected int MaxPageSize = 500;
        protected int DefaultPageSize = 20;

        private int _pageSize;
        private int _pageNumber;
        private string _searchVal = string.Empty;
        private string _sortOrder = string.Empty;
        private string _sortBy = string.Empty;

        public virtual int PageSize
        {
            get => _pageSize;
            set => _pageSize = value < 1 ? DefaultPageSize : Math.Min(Math.Max(value, 1), MaxPageSize);
        }
        public virtual int PageNumber
        {
            get => _pageNumber;
            set => _pageNumber = Math.Max(value, 1);
        }
        public virtual string SearchVal
        {
            get => _searchVal;
            set => _searchVal = String.IsNullOrWhiteSpace(value) ? string.Empty : value;
        }
        public virtual string SortOrder
        {
            get => _sortOrder;
            set => _sortOrder = String.IsNullOrWhiteSpace(value) ? string.Empty : value;
        }
        public virtual  string SortBy
        {
            get => _sortBy;
            set => _sortBy = String.IsNullOrWhiteSpace(value) ? string.Empty : value;

        }
    }
}
