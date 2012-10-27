namespace SwissKip.Web.Handlers
{
    using DapperExtensions;

    using SwissKip.Web.Core;
    using SwissKip.Web.Security;

    public class AccountConfirmHandler
    {
        public void Handle(int id)
        {
            var account = Find(id);
            account.ConfirmEmail();
            Update(account);
            AuthenticationService.SignIn(account);
        }

        private Account Find(int id)
        {
            return Current.Connection.Get<Account>(id);
        }

        private void Update(Account account)
        {
            Current.Connection.Update(account);
        }
    }
}