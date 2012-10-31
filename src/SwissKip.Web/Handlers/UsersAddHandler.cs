namespace SwissKip.Web.Handlers
{
    using System.Collections.Generic;
    using System.Linq;

    using DapperExtensions;

    using Mvc.Mailer;

    using SwissKip.Web.Core;
    using SwissKip.Web.Mailers;
    using SwissKip.Web.Models;

    public class UsersAddHandler
    {
        public void Handle(List<UserCreateModel> witnesses)
        {
            //Session["witnesses"] = witnesses;
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

        private static void AddWitnessToOwner(Account owner, Account witness)
        {
            var ownerWitness = new OwnerWitness(owner.Id, witness.Id);
            Current.Connection.Insert(ownerWitness);
        }

        private static void SendInvitation(string ownerFullName, Account witness)
        {
            var mailer = new DefaultMailer();
            var msg = mailer.CreateAccountWitnessInvitation(witness.Email, witness.Id, witness.FirstName, ownerFullName);
            msg.Send();
        }
    }
}