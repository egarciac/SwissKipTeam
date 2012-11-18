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
                "select FirstName, LastName, Email " +
                "from [User] " +
                "where Id=@id ",
                new { id = this.ownerId }).Single();
        }
    }
}