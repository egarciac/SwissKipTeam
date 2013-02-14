namespace SwissKip.Web.Handlers
{
    using System;
    using System.Linq;
    using Dapper;
    using DapperExtensions;

    using SwissKip.Web.Core;
    using SwissKip.Web.Core.Exceptions;
    using SwissKip.Web.Models;
    using SwissKip.Web.Mailers;
    using Mvc.Mailer;

    public class SignInHandler
    {
         public User Handle(SignInModel form)
         {
             var user = this.Find(form.Username);
             if (user!=null)
             {
                 if (user.Banned)
                 {
                     DateTime fecha = (DateTime)user.ModifiedDate;
                     if (System.DateTime.Now > fecha.AddDays(1))
                     {
                         user.Banned = false;
                         user.ModifiedDate = System.DateTime.Now;
                         user.Tried = 0;
                         Current.Connection.Update(user);
                     }
                     else
                         throw new ValidationException("Your account is still banned");
                 }

                 if (user.PasswordMatches(form.Password))
                    return user;
                 else
                 {
                    user.Tried = user.Tried + 1;
                    Current.Connection.Update(user);
                    if (user.Tried >= 3) //Number of Tries
                    {
                        user.Banned = true;
                        Current.Connection.Update(user);
                        throw new ValidationException("Your account has been banned");
                    }
                 }
                 
             }
             throw new ValidationException("Invalid Username or Password");
         }

         public User Handle2(SignInModel form)
         {
             User user = this.Find2(form.Email);
             if (user!=null)
             {
                 if (form.Password1 != form.Password2)
                     throw new ValidationException("Passwords are not equals.");
                 else
                 {
                     user.Password = form.Password1;
                     user.Tried = 0;
                     user.Blocked = false;
                     Current.Connection.Update(user);
                     return user;
                 }
             }
             throw new ValidationException("Your Email doesn't have any account associated.");
         }

         public User Handle3(BasicInfoModel form, BasicInfoModel data)
         {
            User user = this.Find2(data.Email);

            if (user.Banned)
            {
                DateTime fecha = (DateTime)user.ModifiedDate;
                if (System.DateTime.Now > fecha.AddDays(1))
                {
                    user.Banned = false;
                    user.ModifiedDate = System.DateTime.Now;
                    user.Tried = 0;
                    Current.Connection.Update(user);
                }
                else
                    return user;
            }
            
            if (data.TokenNumber.ToString() != form.TokenNumberNew)
            {
                data.Count = data.Count + 1;
                if (data.Count >= 3)
                {
                    user.Banned = true;
                    user.ModifiedDate = System.DateTime.Now;
                    Current.Connection.Update(user);
                    throw new ValidationException("Blocked Token");
                }
                throw new ValidationException("Invalid Token");
            }
            else
            {
                user.Banned = false;
                user.ModifiedDate = System.DateTime.Now;
                user.Tried = 0;
                Current.Connection.Update(user);
                return user;
            }
         }

         public static int SendInvitation(User owner)
         {
             var mailer = new DefaultMailer();
             System.Random valor = new System.Random();
             int newValue = valor.Next(899999) + 100000;
             var msg = mailer.TokenInformation(owner.Email, owner.FirstName, newValue);
             msg.Send();

             return newValue;
         }

        private User Find(string userName)
        {
            var predicate = Predicates.Field<User>(f => f.UserName, Operator.Eq, userName);
            return Current.Connection.GetList<User>(predicate).SingleOrDefault();
        }

        private User Find2(string email)
        {
            var predicate = Predicates.Field<User>(f => f.Email, Operator.Eq, email);
            return Current.Connection.GetList<User>(predicate).SingleOrDefault();
        }

        public User Find3(int id)
        {
            var predicate = Predicates.Field<User>(f => f.Id, Operator.Eq, id);
            return Current.Connection.GetList<User>(predicate).SingleOrDefault();
        }

        public Master Find4()
        {
            var master = Current.Connection.Query<Master>(
                "select * from [Master] ").First();
            return master;
        }
    }
}