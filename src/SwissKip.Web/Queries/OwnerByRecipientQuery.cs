namespace SwissKip.Web.Queries
{
    using System.Collections.Generic;
    using System.Linq;

    using Dapper;

    using SwissKip.Web.Models;

    public class OwnersByRecipientQuery
    {
        private readonly int recipientId;

        private readonly int ownerId;

        public OwnersByRecipientQuery(int recipientId)
        {
            this.recipientId = recipientId;
        }

        public OwnersByRecipientQuery(int recipientId, int ownerId)
        {
            this.recipientId = recipientId;
            this.ownerId = ownerId;
        }

        public List<OwnerByRecipientModel> Execute()
        {
            var owners = Current.Connection.Query<OwnerByRecipientModel>(
                "select uu.UserId, u.FirstName, u.LastName, uu.UserIdFather, (select o.FirstName + ' ' + o.LastName from [user] o where o.Id= uu.UserIdFather) as 'OwnerFullName' " +
                "from [user] u inner join [User_UserType] uu on u.Id=uu.UserId " +
                "where u.Id =@recipientId", new { recipientId }).ToList();
            return owners;
        }

        public List<RecipientAddModel> ExecuteNew()
        {
            var owners = Current.Connection.Query<RecipientAddModel>(
                "SELECT u.Id, u.FirstName, u.LastName, u.Email " +
                "FROM User_UserType uu INNER JOIN [User] u ON uu.UserId = u.Id " +
                "WHERE UserIdFather=@id AND UserTypeId=2",
                new { id = Current.UserId }).ToList();
            return owners;
        }

        public List<OwnerByWitnessModel> ExecuteNew2()
        {
            var owners = Current.Connection.Query<OwnerByWitnessModel>(
                "select u.FirstName, u.LastName, u.Email from [user] u " +
                "inner join [User_UserType] uu on u.Id=uu.UserId " +
                "where uu.UserIdFather=@ownerId and uu.UserTypeId=3 ", new { ownerId, recipientId }).ToList();
            return owners;
        }
    }
}