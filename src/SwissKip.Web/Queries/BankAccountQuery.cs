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
                "Select AccountId, BankName, BankAccountNumber, Password, Country " +
                "from BankAccount " +
                "where AccountId=@id",
                new { id = this.AccountId }).ToList();
            return owners;
        }
        
    }
}