namespace SwissKip.Web.Core
{
    using System;

    //public enum UserRoles
    //{
    //    Owner,
    //    Witness,
    //    Recipient
    //}

    public class Account
    {
        public Account() { }

        private Account(string firstName, string lastName, string email, string userName, string password, UserRoles accountType)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Email = email;
            this.UserName = userName;
            this.Password = password;
            //this.AddRole(accountType);
        }

        public static Account CreateOwner(string firstName, string lastName, string email, string userName, string password)
        {
            return new Account(firstName, lastName, email, userName, password, UserRoles.Owner);
        }

        public static Account CreateWitness(string firstName, string lastName, string email, string userName, string password)
        {
            return new Account(firstName, lastName, email, userName, password, UserRoles.Witness);
        }

        public static Account CreateRecipient(string firstName, string lastName, string email, string userName, string password)
        {
            return new Account(firstName, lastName, email, userName, password, UserRoles.Dataheir);
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

        //public void AddRole(UserRoles accountType)
        //{
        //    switch (accountType)
        //    {
        //        case UserRoles.Owner:
        //            IsOwner = true;
        //            break;
        //        case UserRoles.Witness:
        //            IsWitness = true;
        //            break;
        //        case UserRoles.Recipient:
        //            IsRecipient = true;
        //            break;
        //    }
        //}

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