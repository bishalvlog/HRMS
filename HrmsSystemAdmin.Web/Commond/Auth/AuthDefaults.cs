namespace HrmsSystemAdmin.Web.Commond.Auth
{
    public static class AuthDefaults
    {
        public const string CookieAuthenticationName = "dgauth";

        // User login path
        public const string LoginPath = "/account/login";

        // User logout path
        public const string LogoutPath = "/account/logout";

        // User access denied/ forbidden path
        public const string AccessDeniedPath = "/error";
    }
}
