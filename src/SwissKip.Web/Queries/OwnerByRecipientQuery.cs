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
                "select u.FirstName, u.LastName  " +
                "from [user] u " +
                "where a.Id =@recipientId", new { recipientId }).ToList();
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
    }
}