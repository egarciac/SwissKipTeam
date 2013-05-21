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
        public User Handle(int? accountId, AccountCreateModel form)
        {
            if (AccountReceivedEmailInvitation(accountId))
            {
                //TODO: ¿Si el usuario no existe, significa que la url fue manipulada?
                //TODO: ¿Si la cuenta ya tiene los datos completos?
                var user = Current.Connection.Get<User>(accountId);

                if (form.Email != user.Email && ExistsAccountWithSameEmail(form.Email))
                    throw new ValidationException("Email", "Your Email already exists");

                if (ExistsAccountWithSameUserName(form.UserName))
                    throw new ValidationException("UserName", "Your UserName already exists");

                user.Map(form, new[] { "Email" });
                //user.DoesNotNeedToConfirmEmail();
                Update(user);
                return user;
            }
            else
            {
                if (ExistsAccountWithSameEmail(form.Email))
                    throw new ValidationException("Email", "Your Email already exists");

                if (ExistsAccountWithSameUserName(form.UserName))
                    throw new ValidationException("UserName", "Your UserName already exists");

                var owner = User.CreateOwner(form.FirstName, form.LastName, form.UserName, form.Password, form.Email, null, null, null, null, 0, 0, null, 0, System.DateTime.Now, 1, 0, null, false, false, true, false, false);
                Save(owner);
                //SendConfirmationEmail(owner);
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
            var predicate = Predicates.Field<User>(f => f.UserName, Operator.Eq, userName);
            return Current.Connection.Count<User>(predicate) >= 1;
        }

        private bool AccountReceivedEmailInvitation(int? accountId)
        {
            return accountId != null;
        }

        private static void Save(User user)
        {
            Current.Connection.Insert(user);
        }

        private static void Update(User user)
        {
            Current.Connection.Update(user);
        }

        //private void SendConfirmationEmail(Account account)
        //{
        //    var mailer = new DefaultMailer();
        //    var msg = mailer.NewAccountConfirmation(account.Email, account.Id, account.FirstName);
        //    msg.Send();
        //}
    }
}