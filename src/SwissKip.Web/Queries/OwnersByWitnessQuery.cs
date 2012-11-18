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
                "select a.FirstName, a.LastName " +
                "from [user] a " +
                "where a.Id=@witnessId", new { witnessId }).ToList();
            return owners;
        }

        public List<WitnessAddModel> ExecuteNew()
        {
            var owners = Current.Connection.Query<WitnessAddModel>(
                "select u.Id, u.FirstName, u.LastName, u.Email " +
                "FROM User_UserType uu INNER JOIN [User] u ON uu.UserId = u.Id " +
                "WHERE UserIdFather=@id AND UserTypeId=3",
                new { id = Current.UserId }).ToList();
            return owners;
        }
    }
}