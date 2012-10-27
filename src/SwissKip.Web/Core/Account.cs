namespace SwissKip.Web.Core
{
    using System;

    public enum AccountRoles
    {
        Owner,
        Witness,
        Recipient
    }

    public class Account
    {
        public Account() { }

        private Account(string firstName, string lastName, string email, string userName, string password, AccountRoles accountType)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Email = email;
            this.UserName = userName;
            this.Password = password;
            this.AddRole(accountType);
        }

        public static Account CreateOwner(string firstName, string lastName, string email, string userName, string password)
        {
            return new Account(firstName, lastName, email, userName, password, AccountRoles.Owner);
        }

        public static Account CreateWitness(string firstName, string lastName, string email, string userName, string password)
        {
            return new Account(firstName, lastName, email, userName, password, AccountRoles.Witness);
        }

        public static Account CreateRecipient(string firstName, string lastName, string email, string userName, string password)
        {
            return new Account(firstName, lastName, email, userName, password, AccountRoles.Recipient);
        }


        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public bool EmailConfirmed { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public DateTime? Birthday { get; set; }

        public int? CountryId { get; set; }

        public bool IsOwner { get; set; }

        public bool IsWitness { get; set; }

        public bool IsRecipient { get; set; }

        public void AddRole(AccountRoles accountType)
        {
            switch (accountType)
            {
                case AccountRoles.Owner:
                    IsOwner = true;
                    break;
                case AccountRoles.Witness:
                    IsWitness = true;
                    break;
                case AccountRoles.Recipient:
                    IsRecipient = true;
                    break;
            }
        }

        public string FullName()
        {
            return this.FirstName + " " + this.LastName;
        }

        public void DoesNotNeedToConfirmEmail()
        {
            this.EmailConfirmed = true;
        }

        public void ConfirmEmail()
        {
            this.EmailConfirmed = true;
        }

        public bool PasswordMatches(string password)
        {
            return this.Password == password;
        }
    }
}