namespace SwissKip.Web.Core
{
    using System;

    public class DigitalAccount
    {
        public DigitalAccount() { }

        private DigitalAccount(int userId, string website, string username, string password, DateTime createdDate, int status)
        {
            this.UserId = userId;
            this.Website = website;
            this.Username = username;
            this.Password = password;
            this.CreatedDate = createdDate;
            this.Status = Status;
        }

        public static DigitalAccount CreateDigitalAccount(int userId, string website, string username, string password, DateTime createdDate, int status)
        {
            return new DigitalAccount(userId, website, username, password, createdDate, status);
        }

        private string AddCharToHidden(string tobehidden)
        {
            string newValue = "*";
            return newValue;
        }

        public int Id { get; set; }

        public int UserId { get; set; }

        public string Website { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public DateTime CreatedDate { get; set; }

        public int Status { get; set; }
    }
}