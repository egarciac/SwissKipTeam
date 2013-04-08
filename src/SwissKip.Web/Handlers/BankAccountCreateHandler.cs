namespace SwissKip.Web.Handlers
{
    using System.Web;

    using DapperExtensions;
    using Mvc.Mailer;

    using SwissKip.Web.Core;
    using SwissKip.Web.Core.Exceptions;
    using SwissKip.Web.Mailers;
    using SwissKip.Web.Models;
    using SwissKip.Web.Security;

    public class BankAccountCreateHandler
    {
        public BankAccount Handle(BankAccountCreateModel form)
        {
            var bankaccount = BankAccount.CreateBankAccount(form.UserId, form.BankName, form.BankAccountNumber, form.Password, form.CountryId, System.DateTime.Now, 1);
            Save(bankaccount);
            //SendEmail(form);
            return bankaccount;
        }

        private static void Save(BankAccount bankaccount)
        {
            Current.Connection.Insert(bankaccount);
        }

        private void SendEmail(BankAccountCreateModel bankaccount)
        {
            var Id = bankaccount.UserId;
            var FirstName = AuthenticationService.GetUser().FirstName;
            var LastName = AuthenticationService.GetUser().LastName;
            var Email = AuthenticationService.GetUser().Email;
            var AccountNumber = bankaccount.BankAccountNumber1;
            var Password = bankaccount.Password1;
            var mailer = new DefaultMailer();
            var msg = mailer.BankAccountNumberEmail(Email, Id, FirstName + LastName, AccountNumber, Password);
            msg.Send();
        }

    }
}