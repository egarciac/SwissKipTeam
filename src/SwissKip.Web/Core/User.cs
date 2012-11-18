namespace SwissKip.Web.Core
{
    using System;

    public class User
    {
        public User() { }

        private User(string firstName, string lastName, string userName, string password, string email, int? countryId, DateTime createdDate, int status, bool isOwner, bool isDataheir, bool isWitness)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.UserName = userName;
            this.Password = password;
            this.Email = email;
            this.CountryId = countryId;
            this.CreatedDate = createdDate;
            this.Status = status;
            this.IsOwner = isOwner;
            this.IsDataheir = isDataheir;
            this.IsWitness = isWitness;
        }

        //Creating Trial User
        public static User CreateOwner(string firstName, string lastName, string userName, string password, string email, int? countryId, DateTime createdDate, int status, bool isOwner, bool isDataheir, bool isWitness)
        {
            return new User(firstName, lastName, userName, password, email, countryId, createdDate, status, isOwner, isDataheir, isWitness);
        }

        //Creating Witness
        public static User CreateWitness(string firstName, string lastName, string userName, string password, string email, int? countryId, DateTime createdDate, int status, bool isOwner, bool isDataheir, bool isWitness)
        {
            return new User(firstName, lastName, userName, password, email, countryId, createdDate, status, isOwner, isDataheir, isWitness);
        }

        //Creating Dataheir
        public static User CreateDataheir(string firstName, string lastName, string userName, string password, string email, int? countryId, DateTime createdDate, int status, bool isOwner, bool isDataheir, bool isWitness)
        {
            return new User(firstName, lastName, userName, password, email, countryId, createdDate, status, isOwner, isDataheir, isWitness);
        }

        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public int? CountryId { get; set; }

        public DateTime? Birthday { get; set; }

        public DateTime CreatedDate { get; set; }

        public int Status { get; set; }

        public bool IsOwner { get; set; }

        public bool IsWitness { get; set; }

        public bool IsDataheir { get; set; }

        public string FullName()
        {
            return this.FirstName + " " + this.LastName;
        }

        public bool PasswordMatches(string password)
        {
            return this.Password == password;
        }

        //public void DoesNotNeedToConfirmEmail()
        //{
        //    this.EmailConfirmed = true;
        //}

        //public void ConfirmEmail()
        //{
        //    this.EmailConfirmed = true;
        //}

        public void AddRole(UserRoles accountType)
        {
            switch (accountType)
            {
                case UserRoles.Owner:
                    IsOwner = true;
                    break;
                case UserRoles.Witness:
                    IsWitness = true;
                    break;
                case UserRoles.Dataheir:
                    IsDataheir = true;
                    break;
            }
        }
    }
}