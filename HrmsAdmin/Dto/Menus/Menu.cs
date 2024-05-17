using System.ComponentModel.DataAnnotations;

namespace HrmsSystemAdmin.Web.Dto.Menus
{
    public class Menu
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int ParentId { get; set; }
        public string MenuUrl { get; set; }
        public bool IsActive { get; set; }
        public int DisplayOrder { get; set; }
        public object ImagePath { get; set; }
    }
    public class GetAllMenu
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int ParentId { get; set; }
        public string MenuUrl { get; set; }
        public bool IsActive { get; set; }
        public int DisplayOrder { get; set; }
        public string ImagePath { get; set; }
    }
    public class Menuwithchield
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int ParentId { get; set; }
        public string MenuUrl { get; set; }
        public bool IsActive { get; set; }
        public int DisplayOrder { get; set; }
        public string ImagePath { get; set; }
        public List<Menuwithchield> child { get; set; }
    }
    public class AddMenu
    {

        [Required]
        public string Title { get; set; }
        [Required]
        public int ParentId { get; set; }
        [Required]
        [RegularExpression("^/.+", ErrorMessage = "Url must Start With /")]
        public string MenuUrl { get; set; }
        public bool IsActive { get; set; }
        public int DisplayOrder { get; set; }
        public IFormFile ImagePath { get; set; }
    }
    public class UpdateMenu
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public int ParentId { get; set; }
        [Required]
        [RegularExpression("^/.+", ErrorMessage = "Url must Start With /")]
        public string MenuUrl { get; set; }

        public bool IsActive { get; set; }
        [Required]
        public int DisplayOrder { get; set; }
        public string ImagePathurl { get; set; }
        public IFormFile ImagePath { get; set; }
    }
}
