using System;
using System.Configuration;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Text.RegularExpressions;

namespace TradeUtility.AD
{
    public class LdapClient : IDisposable
    {
        private PrincipalContext _context;
        private string _domain;
        private string _userName;
        private string _password;

        public LdapClient(string domain, string userName, string password)
        {
            _domain = domain;
            _userName = userName;
            _password = password;
            _context = new PrincipalContext(ContextType.Domain, _domain, userName, _password);
        }

        public void AuthUser()
        {
            try
            {
                using (var user = UserPrincipal.FindByIdentity(_context, IdentityType.SamAccountName, _userName))
                {
                }
            }
            catch (DirectoryServicesCOMException exc)
            {
                LdapErrors errCode = 0;

                try
                {
                    // Unfortunately, the only place to get the LDAP bind error code is in the "data" field of the
                    // extended error message, which is in this format:
                    // 80090308: LdapErr: DSID-0C09030B, comment: AcceptSecurityContext error, data 52e, v893
                    if (!string.IsNullOrEmpty(exc.ExtendedErrorMessage))
                    {
                        Match match = Regex.Match(exc.ExtendedErrorMessage, @" data (?<errCode>[0-9A-Fa-f]+),");
                        if (match.Success)
                        {
                            string errCodeHex = match.Groups["errCode"].Value;
                            errCode = (LdapErrors)Convert.ToInt32(errCodeHex, fromBase: 16);
                        }
                    }
                }
                catch { }

                switch (errCode)
                {
                    case LdapErrors.ERROR_LOGON_FAILURE:
                        throw new Exception("AD帳號密碼錯誤");

                    case LdapErrors.ERROR_PASSWORD_EXPIRED:
                    case LdapErrors.ERROR_PASSWORD_MUST_CHANGE:
                        throw new Exception("你的密碼必須先變更，請先用WINDOW作業系統登入自己的AD帳號去變更AD密碼");

                    case LdapErrors.ERROR_ACCOUNT_LOCKED_OUT:
                        throw new Exception("你的AD帳號已經被鎖定");
                }

                // If the extended error handling doesn't work out, just throw the original exception.
                throw;
            }
        }

        public DateTime? GetUserLastPasswordSet()
        {
            using (var user = UserPrincipal.FindByIdentity(_context, IdentityType.SamAccountName, _userName))
            {
                return user.LastPasswordSet;
            }
        }

        public ValidateResult IsRemindUserToChangePassword()
        {
            var validateResult = new ValidateResult();

            DateTime? lastPasswordSet = GetUserLastPasswordSet();

            if (lastPasswordSet.HasValue)
            {
                DateTime? validPasswordDate = lastPasswordSet?.AddMonths(3);

                if (validPasswordDate.HasValue)
                {
                    // 算出來會和作業系統跳出的密碼過期時間有點誤差，所以再減2
                    int validPasswordRemainDays = validPasswordDate.Value.Subtract(DateTime.Now).Days - 2;
                    if (validPasswordRemainDays > 0 && validPasswordRemainDays <= 14)
                    {
                        validateResult.Result = true;
                        validateResult.ValidPasswordRemainDays = validPasswordRemainDays;
                        return validateResult;
                    }
                }
            }

            validateResult.Result = false;
            validateResult.ValidPasswordRemainDays = 0;

            return validateResult;
        }

        public void ChangeUserPassword(string oldPassword, string newPassword)
        {
            using (var user = UserPrincipal.FindByIdentity(_context, IdentityType.SamAccountName, _userName))
            {
                if (user == null) throw new Exception("無此AD帳號");

                user.ChangePassword(oldPassword, newPassword);
                user.Save();
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }

    public class ValidateResult
    {
        private bool _result = false;
        private int _validPasswordDays = 0;

        public bool Result { get => _result; set => _result = value; }

        public int ValidPasswordRemainDays { get => _validPasswordDays; set => _validPasswordDays = value; }
    }
}