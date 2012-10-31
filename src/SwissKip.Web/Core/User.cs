namespace SwissKip.Web.Core
{
    using System;

    public class User
    {
        public User() { }

        private User(string firstName, string lastName, string email, string userName, string password, DateTime dateCreated, int status)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Email = email;
            this.UserName = userName;
            this.Password = password;
            this.DateCreated = dateCreated;
            this.Status = status;
        }

        public static User CreateOwner(string firstName, string lastName, string email, string userName, string password, DateTime dateCreated, int status)
        {
            return new User(firstName, lastName, email, userName, password, dateCreated, status);
        }

        public static User CreateWitness(string firstName, string lastName, string email, string userName, string password, DateTime dateCreated, int status)
        {
            return new User(firstName, lastName, email, userName, password, dateCreated, status);
        }

        public static User CreateDataheir(string firstName, string lastName, string email, string userName, string password, DateTime dateCreated, int status)
        {
            return new User(firstName, lastName, email, userName, password, dateCreated, status);
        }
        
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public int? CountryId { get; set; }

        public DateTime? Birthday { get; set; }

        public DateTime DateCreated { get; set; }

        public int Status { get; set; }

        public string FullName()
        {
            return this.FirstName + " " + this.LastName;
        }

        public bool PasswordMatches(string password)
        {
            return this.Password == password;
        }
    }
}