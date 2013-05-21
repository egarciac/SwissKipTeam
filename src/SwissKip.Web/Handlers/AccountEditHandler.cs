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
            //account.Birthday = form.Birthday.ToDate();
            account.CountryId = form.CountryId;
            account.Age = form.Age;
            account.City = form.City;
            account.MaritalStatus = form.MaritalStatus;
            Update(account);
        }

        public void Handle2(int id, AccountEditModel form)
        {
            var account = Find(id);
            account.Password = form.Password;
            account.Email = form.Email;
            Update(account);
        }

        private static User Find(int id)
        {
            return Current.Connection.Get<User>(id);
        }

        private static void Update(User account)
        {
            Current.Connection.Update(account);
        }
    }
}