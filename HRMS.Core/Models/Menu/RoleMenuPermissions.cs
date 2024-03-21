using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Core.Models.Menu
{
    public class RoleMenuPermissions
    {
        [Required] public int MenuId { get; set; }

        [Required(AllowEmptyStrings = false)]
        [MaxLength(450)]
        public string Title { get; set; }

        [Required] public int ParentId { get; set; }

        [Required(AllowEmptyStrings = true)]
        [MaxLength(2083)]
        public string MenuUrl { get; set; }

        [Required]
        public int DisplayOrder { get; set; }

        public string ImagePath { get; set; }

        [Required] public bool ViewPer { get; set; }

        [Required] public bool CreatePer { get; set; }

        [Required] public bool UpdatePer { get; set; }

        [Required] public bool DeletePer { get; set; }
    }

    public class MenuWithSubmenus
    {
        [Required] public int MenuId { get; set; }

        [Required] public string Title { get; set; }

        [Required] public int ParentId { get; set; }

        [Required(AllowEmptyStrings = true)] public string MenuUrl { get; set; }

        [Required]
        public int DisplayOrder { get; set; }

        public string ImagePath { get; set; }

        [Required] public bool ViewPer { get; set; }

        [Required] public bool CreatePer { get; set; }

        [Required] public bool UpdatePer { get; set; }

        [Required] public bool DeletePer { get; set; }

        public List<MenuWithSubmenus> SubMenus { get; set; } = new();
    }
}

