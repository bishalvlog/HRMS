using System.ComponentModel.DataAnnotations;

namespace HrmsSystemAdmin.Web.Dto.Users.Auth
{
    public class UserLoginRequest
    {
        [Required]
        public string GrantType { get; set; }
        [Required]
        public string Source { get; set; }
        [Required]
        [Display(Name = "User Name")]
        public string UserNameOrEmail { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool rememberme { get; set; }
        public string RefreshToken { get; set; }
        public bool RememberMe { get; set; }
    }
}
