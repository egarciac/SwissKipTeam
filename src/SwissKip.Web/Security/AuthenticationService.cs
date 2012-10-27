namespace SwissKip.Web.Security
{
    using System.Web.Security;

    using SwissKip.Web.Core;

    public class AuthenticationService
    {
        public static void SignIn(Account account)
        {
            FormsAuthentication.SetAuthCookie(account.Id.ToString(), false);
        }

        public static Account GetUser()
        {
            return Current.User;
        }
    }
}