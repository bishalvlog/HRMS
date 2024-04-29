using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HRMS.Data.Comman.Helpers
{
    public partial  class CommonHelper
    {
        #region Fields 
        private const string _emailExpression = @"^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-||_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+([a-z]+|\d|-|\.{0,1}|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])?([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))$";
        private const string _usernameExpression = @"^[a-zA-Z0-9](_(?!(\.|_))|\.(?!(_|\.))|[a-zA-Z0-9]){6,18}[a-zA-Z0-9]$";

        private const string _alphaNumSpecialPasswordExp_8_32 = @"^(?=.*[a-z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,32}$";
        public const string MessageAlphaNumSpecialPassword_8_32 = "Password must be of minimum of 8 characters and must contain at least one letter, one number, and one special character.";

        private const string _alphaNumUpperSpecialPasswordExp_8_32 = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&#+_])[A-Za-z\d@$!%*?&#+_]{8,32}$";
        public const string MessageAlphaNumUpperSpecialPassword_8_32 = "Password must be of minimum of 8 characters and must contain at least one uppercase letter, one lowercase letter, one number, and one special character.";

        private static readonly Regex _emailRegex;
        private static readonly Regex _usernameRegex;
        private static readonly Regex _alphaNumSpecialPasswordExp_8_32_Regex;
        private static readonly Regex _alphaNumUpperSpecialPassword_8_32_Regex;
        #endregion

        #region methods
        public static bool IsValidEmail(string Email)
        {
            if(String.IsNullOrEmpty(Email)) return false;

            Email = Email.Trim();
             
            return _emailRegex.IsMatch(Email);
        }

        #endregion
    }
}
