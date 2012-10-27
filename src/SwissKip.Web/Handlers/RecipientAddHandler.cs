namespace SwissKip.Web.Handlers
{
    using System.Linq;

    using DapperExtensions;

    using Mvc.Mailer;

    using SwissKip.Web.Core;
    using SwissKip.Web.Mailers;
    using SwissKip.Web.Models;

    public class RecipientAddHandler
    {
        public void Handle(Account owner, RecipientAddModel form)
        {
            //TODO: No se puede agregar como beneficiario a uno mismo
            var account = this.Find(form.Email);
            if (account == null)
            {
                account = Account.CreateRecipient(form.FirstName, form.LastName, form.Email, null, null);
                Save(account);
                AddRecipientToOwner(owner.Id, account.Id);
                SendInvitation(owner.FullName(), account);
            }
            else
            {
                account.AddRole(AccountRoles.Recipient);
                Update(account);
                AddRecipientToOwner(owner.Id, account.Id);
                //TODO: Tambien enviar email indicando que ha sido agregado como testigo
            }
        }

        private Account Find(string email)
        {
            var predicate = Predicates.Field<Account>(f => f.Email, Operator.Eq, email);
            return Current.Connection.GetList<Account>(predicate).SingleOrDefault();
        }

        private static void Save(Account account)
        {
            Current.Connection.Insert(account);
        }

        private static void Update(Account account)
        {
            Current.Connection.Update(account);
        }

        private static void AddRecipientToOwner(int ownerId, int recipientId)
        {
            var recipient = Current.Connection.Get<OwnerRecipient>(ownerId);
            if (recipient==null)
            {
                recipient = new OwnerRecipient(ownerId, recipientId);
                Current.Connection.Insert(recipient);
            }
            else
            {
                recipient.RecipientId = recipientId;
                Current.Connection.Update(recipient);
            }
        }

        private static void SendInvitation(string ownerFullName, Account recipient)
        {
            var mailer = new DefaultMailer();
            var msg = mailer.CreateAccountRecipientInvitation(recipient.Email, recipient.Id, recipient.FirstName, ownerFullName);
            msg.Send();
        }
    }
}