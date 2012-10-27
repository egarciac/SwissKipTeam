namespace SwissKip.Web.Core
{
    using System;

    public class BankAccount
    {
        public BankAccount() { }

        private BankAccount(int accountId, string bankname, string bankaccountnumber, string password, string country)
        {
            this.AccountId = accountId;
            this.BankName = bankname;
            this.BankAccountNumber = bankaccountnumber;
            this.Password = password;
            this.Country = country;
        }

        public static BankAccount CreateBankAccount(int accountId, string bankname, string bankaccountnumber, string password, string country)
        {
            return new BankAccount(accountId, bankname, bankaccountnumber, password, country);
        }

        public int Id { get; set; }

        public int AccountId { get; set; }

        public string BankName { get; set; }

        public string BankAccountNumber { get; set; }

        public string Password { get; set; }

        public string Country { get; set; }

        //public int? CountryId { get; set; }

        //public string BankAccount1 { get; set; }

        //public string BankAccount2 { get; set; }

        //public string Clave1 { get; set; }

        //public string Clave2 { get; set; }

    }
}