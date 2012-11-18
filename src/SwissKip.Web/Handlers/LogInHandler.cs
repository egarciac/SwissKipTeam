namespace SwissKip.Web.Handlers
{
    using System.Linq;

    using DapperExtensions;

    using SwissKip.Web.Core;
    using SwissKip.Web.Core.Exceptions;
    using SwissKip.Web.Models;

    public class SignInHandler
    {
         public User Handle(SignInModel form)
         {
             var user = this.Find(form.Username);
             if (user!=null)
             {
                 if (user.PasswordMatches(form.Password))
                 {
                     return user;
                 }
             }
             throw new ValidationException("Invalid Username or Password");
         }

        private User Find(string userName)
        {
            var predicate = Predicates.Field<User>(f => f.UserName, Operator.Eq, userName);
            return Current.Connection.GetList<User>(predicate).SingleOrDefault();
        }
    }
}