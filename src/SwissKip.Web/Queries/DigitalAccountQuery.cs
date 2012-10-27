namespace SwissKip.Web.Queries
{
    using System.Collections.Generic;
    using System.Linq;

    using Dapper;

    using SwissKip.Web.Models;

    public class DigitalAccountQuery
    {
        private readonly int AccountId;

        public DigitalAccountQuery(int AccountId)
        {
            this.AccountId = AccountId;
        }

        public List<DigitalAccountModel> Execute()
        {
            var owners = Current.Connection.Query<DigitalAccountModel>(
                "select AccountId,WebSite,Username,Password " +
                "from DigitalAccount " + 
                "where AccountId=@id",
                new { id = this.AccountId }).ToList();
            return owners;
        }

    }
}