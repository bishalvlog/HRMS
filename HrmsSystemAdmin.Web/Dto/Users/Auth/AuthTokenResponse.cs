namespace HrmsSystemAdmin.Web.Dto.Users.Auth
{
    public class AuthTokenResponse : HrmsApiResponse
    {
        public string Username { get; set; }
        public string AccessToken { get; set; }
        public string TokenType { get; set; }
        public string RefreshToken { get; set; }
        public long ExpiresIn { get; set; }
    }
}
