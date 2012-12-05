namespace SwissKip.Web.Queries
{
    using System.Collections.Generic;
    using System.Linq;

    using Dapper;

    using SwissKip.Web.Models;

    public class BankAccountQuery
    {
        private readonly int AccountId;

        public BankAccountQuery(int AccountId)
        {
            this.AccountId = AccountId;
        }
        
        public List<BankAccountModel> Execute()
        {
            var owners = Current.Connection.Query<BankAccountModel>(
                "Select ba.Id, ba.BankName, ba.BankAccountNumber, ba.Password, c.Name as Country  " +
                "From [dbo].[BankAccount] ba  " +
                "inner join [dbo].[Country] c on ba.CountryId = c.Id  " +
                "where UserId=@id",
                new { id = this.AccountId }).ToList();
            return owners;
        }
        
    }
}