namespace SwissKip.Web.Queries
{
    using System.Collections.Generic;
    using System.Linq;

    using Dapper;

    using SwissKip.Web.Models;

    public class OwnersByRecipientQuery
    {
        private readonly int recipientId;

        public OwnersByRecipientQuery(int recipientId)
        {
            this.recipientId = recipientId;
        }

        public List<OwnerByRecipientModel> Execute()
        {
            var owners = Current.Connection.Query<OwnerByRecipientModel>(
                "select a.FirstName,a.LastName  " +
                "from OwnerRecipient ow " +
                "inner join Account a " +
                "on ow.OwnerId=a.Id " +
                "where ow.RecipientId=@recipientId", new { recipientId }).ToList();
            return owners;
        }

        public List<WitnessAddModel> ExecuteNew()
        {
            var owners = Current.Connection.Query<WitnessAddModel>(
                "select Id, FirstName, LastName, Email " +
                "from dbo.Account where Id in " +
                "(select WitnessId  from " +
                "dbo.OwnerWitness where OwnerId=@id )",
                new { id = Current.UserId }).ToList();
            return owners;
        }
    }
}