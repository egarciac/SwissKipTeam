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
                "Select ba.Id, b.Description, ba.BankAccountNumber, ba.Password, ba.CountryId  " +
                "From [dbo].[BankAccount] ba Inner Join [dbo].[Bank] b ON ba.BankId = b.BankId " +
                "where UserId=@id",
                new { id = this.AccountId }).ToList();
            return owners;
        }
        
    }
}