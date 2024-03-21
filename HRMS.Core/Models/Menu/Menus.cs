using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Core.Models.Menu
{
    public class Menus
    {
        private string _menuUrl = string.Empty;
        public int Id { get; set; }
        public string Title { get; set; }   
        public int ParentId { get; set; }   
        public string MenuUrl
        {
            get => _menuUrl;
            set => _menuUrl = String.IsNullOrWhiteSpace(value)? _menuUrl: value;
        }
        public  bool IsActive {  get; set; }    
        public int DisplayOrder { get; set; }  
        public string ImagePath { get; set; }   
    }
}
