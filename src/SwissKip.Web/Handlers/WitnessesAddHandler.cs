namespace SwissKip.Web.Handlers
{
    using System.Collections.Generic;
    using System.Linq;

    using DapperExtensions;

    using Mvc.Mailer;

    using SwissKip.Web.Core;
    using SwissKip.Web.Mailers;
    using SwissKip.Web.Models;
    using SwissKip.Web.Queries;

    public class WitnessesAddHandler
    {
        public void Handle(User owner, User witness)
        {
            //foreach (var witnessForm in witnessesForm)
            //{
            //    //TODO: No se puede agregar como testigo a uno mismo
            //    var user = this.Find(witnessForm.Email);
            //    if (user == null)
            //    {
            //        user = User.CreateWitness(witnessForm.FirstName, witnessForm.LastName, null, null, witnessForm.Email, 0, System.DateTime.Now, 1, false, false, true);
            //        Save(user);
            //        AddWitnessToOwner(owner, user);
                    SendInvitation(owner, witness);
                //}
                //else
                //{
                //    user.AddRole(UserRoles.Witness);
                //    Update(user);
                //    AddWitnessToOwner(owner, user);
                //    //TODO: Tambien enviar email indicando que ha sido agregado como testigo
                //}
            //}
        }

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

        private static void AddWitnessToOwner(User owner, User witness)
        {
            var ownerWitness = new OwnerWitness(owner.Id, witness.Id);
            Current.Connection.Insert(ownerWitness);
        }

        private static void SendInvitation(User owner, User witness)
        {
            var mailer = new DefaultMailer();
            var msg = mailer.CreateAccountWitnessInvitation(witness.Email, witness.Id, witness.FirstName, owner.FullName());
            msg.Send();
        }

        public void SendDeadReport(int UserId, User FatherInfo)
        {
            var mailer = new DefaultMailer();
            var w = new OwnersByWitnessQuery(UserId, FatherInfo.Id).ExecuteNew2();
            for (int i = 0; i < w.Count; i++)
            {
                var msg = mailer.SendWitnessDeadReport(FatherInfo.FullName(), w[i].FirstName + " " + w[i].LastName, w[i].Email);
                msg.Send();
            }
        }
    }
}