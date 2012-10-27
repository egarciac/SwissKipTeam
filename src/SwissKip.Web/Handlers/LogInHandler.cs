namespace SwissKip.Web.Handlers
{
    using System.Linq;

    using DapperExtensions;

    using SwissKip.Web.Core;
    using SwissKip.Web.Core.Exceptions;
    using SwissKip.Web.Models;

    public class SignInHandler
    {
         public Account Handle(SignInModel form)
         {
             var account = this.Find(form.Username);
             if (account!=null)
             {
                 if (account.PasswordMatches(form.Password))
                 {
                     return account;
                 }
             }
             throw new ValidationException("Invalid Username or Password");
         }

        private Account Find(string userName)
        {
            var predicate = Predicates.Field<Account>(f => f.UserName, Operator.Eq, userName);
            return Current.Connection.GetList<Account>(predicate).SingleOrDefault();
        }
    }
}