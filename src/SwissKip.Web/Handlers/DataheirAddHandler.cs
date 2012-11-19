namespace SwissKip.Web.Handlers
{
    using System.Linq;

    using DapperExtensions;

    using Mvc.Mailer;

    using SwissKip.Web.Core;
    using SwissKip.Web.Mailers;
    using SwissKip.Web.Models;
    using SwissKip.Web.Queries;

    public class DataheirAddHandler
    {
        public void Handle(User owner, User dataheir)
        {
            //TODO: No se puede agregar como beneficiario a uno mismo
            //var user = this.Find(dataheir.Email);
            //if (user == null)
            //{
                //user = User.CreateDataheir(form.FirstName, form.LastName, null, null, form.Email, 0, System.DateTime.Now, 1, false, true, false);
                //Save(user);
                //AddDataheirToOwner(owner.Id, user.Id);
                SendInvitation(owner, dataheir);
            //}
            //else
            //{
                //user.AddRole(UserRoles.Dataheir);
                //Update(user);
                //AddDataheirToOwner(owner.Id, user.Id);
                //TODO: Tambien enviar email indicando que ha sido agregado como testigo
            //}
        }

        //public void Handle2(User owner)
        //{
        //    SendDeadReport(owner);
        //}

        private User Find(string email)
        {
            var predicate = Predicates.Field<User>(f => f.Email, Operator.Eq, email);
            return Current.Connection.GetList<User>(predicate).SingleOrDefault();
        }

        public User_UserType FindIdOwner(int currentId)
        {
            var predicate = Predicates.Field<User_UserType>(f => f.UserId, Operator.Eq, currentId);
            return Current.Connection.GetList<User_UserType>(predicate).SingleOrDefault();
        }

        public User FindFatherInfo(int fatherId)
        {
            var predicate = Predicates.Field<User>(f => f.Id, Operator.Eq, fatherId);
            return Current.Connection.GetList<User>(predicate).SingleOrDefault();
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

        private static void Save(User user)
        {
            Current.Connection.Insert(user);
        }

        private static void Update(User user)
        {
            Current.Connection.Update(user);
        }

        private static void AddDataheirToOwner(int ownerId, int recipientId)
        {
            //var recipient = Current.Connection.Get<OwnerDataheir>(ownerId);
            //if (recipient==null)
            //{
            //    recipient = new OwnerDataheir(ownerId, recipientId);
            //    Current.Connection.Insert(recipient);
            //}
            //else
            //{
            //    recipient.DataheirId = recipientId;
            //    Current.Connection.Update(recipient);
            //}
        }

        private static void SendInvitation(User owner, User dataheir)
        {
            var mailer = new DefaultMailer();
            var msg = mailer.CreateAccountDataheirInvitation(dataheir.Email, dataheir.Id, dataheir.FirstName, owner.FullName());
            msg.Send();
        }

        public void SendDeadReport(int UserId, User FatherInfo)
        {
            var mailer = new DefaultMailer();
            var w = new OwnersByRecipientQuery(UserId, FatherInfo.Id).ExecuteNew2();
            for (int i = 0; i < w.Count; i++)
            {
                var msg = mailer.SendWitnessDeadReport(FatherInfo.FullName(), w[i].FirstName + " " + w[i].LastName, w[i].Email);
                msg.Send();
            }
        }
    }
}