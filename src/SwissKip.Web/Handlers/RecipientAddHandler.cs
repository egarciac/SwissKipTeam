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
        public void Handle(User owner, RecipientAddModel form)
        {
            //TODO: No se puede agregar como beneficiario a uno mismo
            var user = this.Find(form.Email);
            if (user == null)
            {
                user = User.CreateDataheir(form.FirstName, form.LastName, null, null, form.Email, 0, System.DateTime.Now, 1, false, true, false);
                Save(user);
                AddRecipientToOwner(owner.Id, user.Id);
                SendInvitation(owner.FullName(), user);
            }
            else
            {
                user.AddRole(UserRoles.Dataheir);
                Update(user);
                AddRecipientToOwner(owner.Id, user.Id);
                //TODO: Tambien enviar email indicando que ha sido agregado como testigo
            }
        }

        private User Find(string email)
        {
            var predicate = Predicates.Field<User>(f => f.Email, Operator.Eq, email);
            return Current.Connection.GetList<User>(predicate).SingleOrDefault();
        }

        private static void Save(User user)
        {
            Current.Connection.Insert(user);
        }

        private static void Update(User user)
        {
            Current.Connection.Update(user);
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

        private static void SendInvitation(string ownerFullName, User recipient)
        {
            var mailer = new DefaultMailer();
            var msg = mailer.CreateAccountDataheirInvitation(recipient.Email, recipient.Id, recipient.FirstName, ownerFullName);
            msg.Send();
        }
    }
}