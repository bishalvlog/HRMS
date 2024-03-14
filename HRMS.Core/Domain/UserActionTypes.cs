using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Core.Domain
{
    public class UserActionTypes
    {
        public const string ActionUserLoggedIn = "Logged in";
        public const string ActionPasswordResetRequset = "Password reset requested";
        public const string ActionPasswordReset = "Password reset";
        public const string ActionPasswordChanged = "Password changed";
        public const string ActionTxnPinChanged = "Transaction pin changed";
        public const string ActionUpdateGlobalSettings = "Updated global settings";
        public const string ActionCreateVerificationType = "Created verification type";
        public const string ActionUpdatedVerificationType = "Updated verification type";
        public const string ActionResetGlobalSettings = "Reset global settings";
        public const string ActionCreatedUser = "Created user";
        public const string ActionCreatedMerchant = "Created merchant";
        public const string ActionUpdatedMerchant = "Updated merchant";
        public const string ActionUpdatedUser = "Updated user";
        public const string ActionDeletedUser = "Deleted user";
        public const string ActionAssignedUserRole = "Assigned role to user";
        public const string ActionResetUserPassword = "Reset user password";
        public const string ActionCreatedRole = "Created Role";
        public const string ActionUpdatedRole = "Updated Role";
        public const string ActionDeletedRole = "Deleted Role";
        public const string ActionCreatedTxnCharge = "Created Transactional Charge";
        public const string ActionUpdatedTxnCharge = "Updated Transactional Charge";
        public const string ActionDeletedTxnCharge = "Deleted Transactional Charge";
        public const string ActionUpdatedKyc = "Updated KYC";
        public const string ActionRequestedMobileOTP = "Requested mobile OTP";
        public const string ActionRequestedEmailOTP = "Requested email OTP";
        public const string ActionCreatedLoantype = "Created Loantype";
        public const string ActionUpdatedLoantype = "Updated Loantype";
        public const string ActionDeletedLoantype = "Deleted Loantype";
        public const string ActionCreatedRate = "Created rate";
        public const string ActionUpdatedRate = "Updated rate";
        public const string ActionDeletedRate = "Deleted rate";
        public const string ActionCreatedMenu = "Created menu";
        public const string ActionUpdatedMenu = "Updated menu";
        public const string ActionDeletedMenu = "Deleted menu";
        public const string ActionRoleMenu = "Updated role menu";
    }
}
