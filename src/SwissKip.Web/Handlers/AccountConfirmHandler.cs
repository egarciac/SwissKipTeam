namespace SwissKip.Web.Handlers
{
    using DapperExtensions;

    using SwissKip.Web.Core;
    using SwissKip.Web.Security;

    public class AccountConfirmHandler
    {
        public void Handle(int id)
        {
            var user = Find(id);
            //user.ConfirmEmail();
            //Update(user);
            AuthenticationService.SignIn(user);
        }

        private User Find(int id)
        {
            return Current.Connection.Get<User>(id);
        }

        private void Update(User user)
        {
            Current.Connection.Update(user);
        }
    }
}