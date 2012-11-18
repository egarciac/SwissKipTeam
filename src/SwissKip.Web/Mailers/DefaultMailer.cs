using Mvc.Mailer;
using System.Net.Mail;

namespace SwissKip.Web.Mailers
{
    public class DefaultMailer : MailerBase
    {
        public virtual MailMessage NewAccountConfirmation(string userEmail, int userId, string userFullName)
        {
            var mailMessage = new MailMessage { Subject = "Registration Confirmation" };
            mailMessage.To.Add(userEmail);
            ViewBag.AccountId = userId;
            ViewBag.FullName = userFullName;
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

        public virtual MailMessage CreateAccountDataheirInvitation(string dataheirEmail, int invitationId, string dataheirFirstName, string ownerFullName)
        {
            var mailMessage = new MailMessage { Subject = "Invitation to be a Dataheir" };
            mailMessage.To.Add(dataheirEmail);
            ViewBag.InvitationId = invitationId;
            ViewBag.RecipientFirstName = dataheirFirstName;
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