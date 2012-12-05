namespace SwissKip.Web.Core
{
    using System;

    public class BankAccount
    {
        public BankAccount() { }

        private BankAccount(int userId, string bankname, string bankaccountnumber, string password, int? countryId, DateTime createdDate, int status)
        {
            this.UserId = userId;
            this.BankName = bankname;
            this.BankAccountNumber = bankaccountnumber;
            this.Password = password;
            this.CountryId = countryId;
            this.CreatedDate = createdDate;
            this.Status = status;
        }

        public static BankAccount CreateBankAccount(int userId, string bankname, string bankaccountnumber, string password, int? countryId, DateTime createdDate, int status)
        {
            return new BankAccount(userId, bankname, bankaccountnumber, password, countryId, createdDate, status);
        }

        public int Id { get; set; }

        public int UserId { get; set; }

        public string BankName { get; set; }

        public string BankAccountNumber { get; set; }

        public string Password { get; set; }

        //public string Country { get; set; }

        public int? CountryId { get; set; }

        public DateTime CreatedDate { get; set; }

        public int Status { get; set; }

    }
}