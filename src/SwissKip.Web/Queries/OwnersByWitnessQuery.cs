namespace SwissKip.Web.Queries
{
    using System.Collections.Generic;
    using System.Linq;

    using Dapper;

    using SwissKip.Web.Models;

    public class OwnersByWitnessQuery
    {
        private readonly int ownerId, witnessId;

        public OwnersByWitnessQuery(int witnessId)
        {
            this.witnessId = witnessId;
        }

        public OwnersByWitnessQuery(int witnessId, int ownerId)
        {
            this.witnessId = witnessId;
            this.ownerId = ownerId;
        }

        public List<OwnerByWitnessModel> Execute()
        {
            var owners = Current.Connection.Query<OwnerByWitnessModel>(
                "select uu.UserId, u.FirstName, u.LastName, u.Email, uu.UserIdFather, (select o.FirstName + ' ' + o.LastName from [user] o where o.Id= uu.UserIdFather) as 'OwnerFullName' " +
                "from [user] u inner join [User_UserType] uu on u.Id=uu.UserId " +
                "where u.Id=@witnessId", new { witnessId }).ToList();
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

        public List<OwnerByWitnessModel> ExecuteNew2()
        {
            var owners = Current.Connection.Query<OwnerByWitnessModel>(
                "select u.FirstName, u.LastName, u.Email from [user] u " +
                "inner join [User_UserType] uu on u.Id=uu.UserId " +
                "where uu.UserIdFather=@ownerId and uu.UserId<>@witnessId and uu.UserTypeId=3 ", new { ownerId, witnessId }).ToList();
            return owners;
        }
    }
}