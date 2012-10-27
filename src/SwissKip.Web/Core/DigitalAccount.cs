namespace SwissKip.Web.Core
{
    using System;

    public class DigitalAccount
    {
        public DigitalAccount() { }

        private DigitalAccount(int accountid, string website, string username, string password)
        {
            this.AccountId = accountid;
            this.Website = website;
            this.Username = username;
            this.Password = password;
        }

        public static DigitalAccount CreateDigitalAccount(int accountid, string website, string username, string password)
        {
            return new DigitalAccount(accountid, website, username, password);
        }

        private string AddCharToHidden(string tobehidden)
        {
            string newValue = "*";
            return newValue;
        }

        public int Id { get; set; }

        public int AccountId { get; set; }

        public string Website { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

    }
}