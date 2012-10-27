using Mvc.Mailer;
using System.Net.Mail;

namespace SwissKip.Web.Mailers
{
    public class DefaultMailer : MailerBase
    {
        public virtual MailMessage NewAccountConfirmation(string accountEmail, int accountId, string accountFirstName)
        {
            var mailMessage = new MailMessage { Subject = "Registration Confirmation" };
            mailMessage.To.Add(accountEmail);
            ViewBag.AccountId = accountId;
            ViewBag.AccountFirstName = accountFirstName;
            PopulateBody(mailMessage, viewName: "NewAccountConfirmation");

            return mailMessage;
        }

        public virtual MailMessage CreateAccountWitnessInvitation(string witnessEmail, int invitationId, string witnessFirstName, string ownerFullName)
        {
            var mailMessage = new MailMessage { Subject = "Invitation to be a Witness" };
            mailMessage.To.Add(witnessEmail);
            ViewBag.InvitationId = invitationId;
            ViewBag.WitnessFirstName = witnessFirstName;
            ViewBag.OwnerFullName = ownerFullName;
            PopulateBody(mailMessage, viewName: "CreateAccountWitnessInvitation");

            return mailMessage;
        }

        public virtual MailMessage CreateAccountRecipientInvitation(string recipientEmail, int invitationId, string recipientFirstName, string ownerFullName)
        {
            var mailMessage = new MailMessage { Subject = "Invitation to be a Recipient" };
            mailMessage.To.Add(recipientEmail);
            ViewBag.InvitationId = invitationId;
            ViewBag.RecipientFirstName = recipientFirstName;
            ViewBag.OwnerFullName = ownerFullName;
            PopulateBody(mailMessage, viewName: "CreateAccountRecipientInvitation");

            return mailMessage;
        }

        public virtual MailMessage BankAccountNumberEmail(string Email, int Id, string FirstName, string bankAccountNumber, string password)
        {
            var mailMessage = new MailMessage { Subject = "New Bank Account Number Created" };
            mailMessage.To.Add(Email);
            ViewBag.AccountId = Id;
            ViewBag.FirstName = FirstName;
            ViewBag.BankAccountNumber = bankAccountNumber;
            ViewBag.Password = password;
            PopulateBody(mailMessage, viewName: "SendBankAccountNumber");

            return mailMessage;
        }
    }
}