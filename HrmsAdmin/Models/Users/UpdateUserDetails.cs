namespace HrmsSystemAdmin.Web.Model.Users
{
    public class UpdateUserDetails
    {
        public int id { get; set; }
        public string fullName { get; set; }
        public string address { get; set; }
        public int gender { get; set; }
        public string department { get; set; }
        public DateTime? dateOfBirth { get; set; }
        public DateTime? dateOfJoining { get; set; }
        public IFormFile ProfileImage { get; set; }
        public bool isActive { get; set; }
    }
}
