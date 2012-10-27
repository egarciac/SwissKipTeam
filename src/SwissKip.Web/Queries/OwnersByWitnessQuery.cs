namespace SwissKip.Web.Queries
{
    using System.Collections.Generic;
    using System.Linq;

    using Dapper;

    using SwissKip.Web.Models;

    public class OwnersByWitnessQuery
    {
        private readonly int witnessId;

        public OwnersByWitnessQuery(int witnessId)
        {
            this.witnessId = witnessId;
        }

        public List<OwnerByWitnessModel> Execute()
        {
            var owners = Current.Connection.Query<OwnerByWitnessModel>(
                "select a.FirstName,a.LastName, ow.Confirmed, ow.OwnerDied " +
                "from OwnerWitness ow " +
                "inner join Account a " +
                "on ow.OwnerId=a.Id " +
                "where ow.WitnessId=@witnessId", new { witnessId }).ToList();
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