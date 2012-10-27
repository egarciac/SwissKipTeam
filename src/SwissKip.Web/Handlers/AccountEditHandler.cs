namespace SwissKip.Web.Handlers
{
    using DapperExtensions;

    using SwissKip.Web.Core;
    using SwissKip.Web.Models;

    public class AccountEditHandler
    {
        public void Handle(int id, AccountEditModel form)
        {
            var account = Find(id);
            account.FirstName = form.FirstName;
            account.LastName = form.LastName;
            account.Birthday = form.Birthday.ToDate();
            account.CountryId = form.CountryId;
            Update(account);
        }

        public void Handle2(int id, AccountEditModel form)
        {
            var account = Find(id);
            account.Password = form.Password;
            account.Email = form.Email;
            Update(account);
        }

        private static Account Find(int id)
        {
            return Current.Connection.Get<Account>(id);
        }

        private static void Update(Account account)
        {
            Current.Connection.Update(account);
        }
    }
}