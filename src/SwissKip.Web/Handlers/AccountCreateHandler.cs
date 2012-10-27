namespace SwissKip.Web.Handlers
{
    using DapperExtensions;
    using Mvc.Mailer;

    using SwissKip.Web.Core;
    using SwissKip.Web.Core.Exceptions;
    using SwissKip.Web.Helpers;
    using SwissKip.Web.Mailers;
    using SwissKip.Web.Models;

    public class AccountCreateHandler
    {
        public Account Handle(int? accountId, AccountCreateModel form)
        {
            if (AccountReceivedEmailInvitation(accountId))
            {
                //TODO: ¿Si el usuario no existe, significa que la url fue manipulada?
                //TODO: ¿Si la cuenta ya tiene los datos completos?
                var account = Current.Connection.Get<Account>(accountId);

                if (form.Email != account.Email && ExistsAccountWithSameEmail(form.Email))
                    throw new ValidationException("Email", "Your Email already exists");

                if (ExistsAccountWithSameUserName(form.Username))
                    throw new ValidationException("UserName", "Your UserName already exists");

                account.Map(form, new[] { "Email" });
                account.DoesNotNeedToConfirmEmail();
                Update(account);
                return account;
            }
            else
            {
                if (ExistsAccountWithSameEmail(form.Email))
                    throw new ValidationException("Email", "Your Email already exists");

                if (ExistsAccountWithSameUserName(form.Username))
                    throw new ValidationException("UserName", "Your UserName already exists");

                var owner = Account.CreateOwner(form.FirstName, form.LastName, form.Email, form.Username, form.Password);
                Save(owner);
                SendConfirmationEmail(owner);
                return owner;
            }

        }

        private bool ExistsAccountWithSameEmail(string email)
        {
            var predicate = Predicates.Field<Account>(f => f.Email, Operator.Eq, email);
            return Current.Connection.Count<Account>(predicate) >= 1;
        }

        private bool ExistsAccountWithSameUserName(string userName)
        {
            var predicate = Predicates.Field<Account>(f => f.UserName, Operator.Eq, userName);
            return Current.Connection.Count<Account>(predicate) >= 1;
        }

        private bool AccountReceivedEmailInvitation(int? accountId)
        {
            return accountId != null;
        }

        private static void Save(Account account)
        {
            Current.Connection.Insert(account);
        }

        private static void Update(Account account)
        {
            Current.Connection.Update(account);
        }

        private void SendConfirmationEmail(Account account)
        {
            var mailer = new DefaultMailer();
            var msg = mailer.NewAccountConfirmation(account.Email, account.Id, account.FirstName);
            msg.Send();
        }
    }
}