namespace SwissKip.Web.Handlers
{
    using DapperExtensions;
    using Mvc.Mailer;

    using SwissKip.Web.Core;
    using SwissKip.Web.Core.Exceptions;
    using SwissKip.Web.Helpers;
    using SwissKip.Web.Mailers;
    using SwissKip.Web.Models;

    public class UserCreateHandler
    {
        public User Handle(User user)
        {
            var user1 = Current.Connection.Get<User>(user.Id);
            SendConfirmationEmail(user);
            return user1;

            //if (UserReceivedEmailInvitation(user.Status))
            //{
                //TODO: ¿Si el usuario no existe, significa que la url fue manipulada?
                //TODO: ¿Si la cuenta ya tiene los datos completos?
                
            //    if (form.Email != user.Email && ExistsUserWithSameEmail(form.Email))
            //        throw new ValidationException("Email", "Your Email already exists");

            //    if (ExistsUserWithSameUserName(form.Username))
            //        throw new ValidationException("UserName", "Your UserName already exists");

            //    user.Map(form, new[] { "Email" });
            //    //user.DoesNotNeedToConfirmEmail();
            //    Update(user);
            //    return user;
            //}
            //else
            //{
            //    //if (ExistsUserWithSameEmail(form.Email))
            //    //    throw new ValidationException("Email", "Your Email already exists");

            //    //if (ExistsUserWithSameUserName(form.Username))
            //    //    throw new ValidationException("UserName", "Your UserName already exists");

                //var owner = User.CreateOwner(form.FirstName, form.LastName, form.Username, form.Password, form.Email, 0, System.DateTime.Now, 1, true, false, false);
            //    //Save(owner);
            //    SendConfirmationEmail(owner);
            //    return user1;
            //}

        }

        public bool ExistsUserWithSameEmail(string email)
        {
            var predicate = Predicates.Field<User>(f => f.Email, Operator.Eq, email);
            return Current.Connection.Count<User>(predicate) >= 1;
        }

        public bool ExistsUserWithSameUserName(string userName)
        {
            var predicate = Predicates.Field<User>(f => f.UserName, Operator.Eq, userName);
            return Current.Connection.Count<User>(predicate) >= 1;
        }

        private bool UserReceivedEmailInvitation(int? userStatus)
        {
            return userStatus > 0;
        }

        private static void Save(User user)
        {
            Current.Connection.Insert(user);
        }

        public void Update(User user)
        {
            Current.Connection.Update(user);
        }

        public void SendConfirmationEmail(User user)
        {
            var mailer = new DefaultMailer();
            var msg = mailer.NewAccountConfirmation(user.Email, user.Url, user.FullName());
            msg.Send();
        }
    }
}