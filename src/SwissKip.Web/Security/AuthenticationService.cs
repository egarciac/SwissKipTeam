namespace SwissKip.Web.Security
{
    using System.Web.Security;

    using SwissKip.Web.Core;

    public class AuthenticationService
    {
        public static void SignIn(User user)
        {
            FormsAuthentication.SetAuthCookie(user.Id.ToString(), false);
        }

        public static User GetUser()
        {
            return Current.User;
        }
    }
}