using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
    public class MenuAddUpdate
    {
        private string _menuUrl = string.Empty;
        public long Id { get; set; }
        [Required(AllowEmptyStrings = false)]
        [MaxLength(450)]
        public string Title { get; set; }
        [Required] public int ParentId { get; set; }
        public string MenuUrl 
        {
            get => _menuUrl;
            set => _menuUrl = string.IsNullOrWhiteSpace(value)? _menuUrl: value;
        }
        [Required] public bool IsActive { get; set; }
        [Required] public int DisplayOrder { get; set; }
        public string ImagePath { get;set; }
        public DateTime? CreatedLocalDate { get; set; }
        public DateTime? CreatedUtcDate { get; set; }
        public string CreatedNepaliDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedLocalDate { get; set; }
        public DateTime? UpdatedUtcDate { get; set; }
        public string UpdatedNepaliDate { get; set; }
        public string UpdatedBy { get; set; }
    }
    public class MenuModel
    {
        private string _menuUrl = string.Empty;

        [Required(AllowEmptyStrings = false)]
        [MaxLength(450)]
        public string Title { get; set; }

        [Required]
        public int ParentId { get; set; }

        [MaxLength(2083)]
        public string MenuUrl
        {
            get => _menuUrl;
            set => _menuUrl = string.IsNullOrEmpty(value) ? _menuUrl : value;
        }

        [Required]
        public bool IsActive { get; set; }

        [Required]
        public int DisplayOrder { get; set; }

        public IFormFile MenuImage { get; set; }
    }

}
