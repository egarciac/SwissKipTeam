namespace SwissKip.Web.Queries
{
    using System.Linq;

    using Dapper;

    using SwissKip.Web.Models;

    public class OwnerPanelQuery
    {
        private readonly int ownerId;

        public OwnerPanelQuery(int ownerId)
        {
            this.ownerId = ownerId;
        }

        public OwnerPanelModel Execute()
        {
            return new OwnerPanelModel
                {
                    MyAccountWidget = this.MyAccountWidgetQuery()
                };
        }

        private MyAccountWidgetModel MyAccountWidgetQuery()
        {
            return Current.Connection.Query<MyAccountWidgetModel>(
                "select FirstName, LastName, Email, IsOwner, IsDataheir, IsWitness, " +
                "(select FirstName + ' ' + LastName  " +
	            "from [dbo].[User_UserType] uut inner join [dbo].[User] u2 on " +
	            "u2.Id= uut.UserId " +
	            "where UserIdFather=@id and UserTypeId=2) as 'Dataheir', " +
	            "(select COUNT(*)*20 from [dbo].[User_UserType] " +
	            "where UserIdFather = @id and UserTypeId=3 and EmailConfirmed=1) as 'Confirmed' " +
                "From [dbo].[User] u " +
                "where U.Id=@id ",
                new { id = this.ownerId }).Single();
        }
    }
}