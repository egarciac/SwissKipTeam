namespace SwissKip.Web.Core
{
    using System;

    public class User
    {
        public User() { }

        private User(string firstName, string lastName, string userName, string password, string email, string age, string city, int? countryId, string maritalstatus, int? iconId, int? colourId, string secretPhrase, int? tokenNumber, DateTime createdDate, int status, int tried, string url, bool blocked, bool banned, bool isOwner, bool isDataheir, bool isWitness)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.UserName = userName;
            this.Password = password;
            this.Email = email;
            this.Age = age;
            this.City = city;
            this.CountryId = countryId;
            this.MaritalStatus = maritalstatus;
            this.IconId = iconId;
            this.ColourId = colourId;
            this.SecretPhrase = secretPhrase;
            this.TokenNumber = tokenNumber;
            this.CreatedDate = createdDate;
            this.Status = status;
            this.Tried = tried;
            this.Url = url;
            this.Blocked = blocked;
            this.Banned = banned;
            this.IsOwner = isOwner;
            this.IsDataheir = isDataheir;
            this.IsWitness = isWitness;
        }

        //Creating Trial User
        public static User CreateOwner(string firstName, string lastName, string userName, string password, string email, string age, string city, int? countryId, string maritalstatus, int? iconId, int? colourId, string secretPhrase, int? tokenNumber, DateTime createdDate, int status, int tried, string url, bool blocked, bool banned, bool isOwner, bool isDataheir, bool isWitness)
        {
            return new User(firstName, lastName, userName, password, email, age, city, countryId, maritalstatus, iconId, colourId, secretPhrase, tokenNumber, createdDate, status, tried, url, blocked, banned, isOwner, isDataheir, isWitness);
        }

        //Creating Witness
        public static User CreateWitness(string firstName, string lastName, string userName, string password, string email, string age, string city, int? countryId, string maritalstatus, int? iconId, int? colourId, string secretPhrase, int? tokenNumber, DateTime createdDate, int status, int tried, string url, bool blocked, bool banned, bool isOwner, bool isDataheir, bool isWitness)
        {
            return new User(firstName, lastName, userName, password, email, age, city, countryId, maritalstatus, iconId, colourId, secretPhrase, tokenNumber, createdDate, status, tried, url, blocked, banned, isOwner, isDataheir, isWitness);
        }

        //Creating Dataheir
        public static User CreateDataheir(string firstName, string lastName, string userName, string password, string email, string age, string city, int? countryId, string maritalstatus, int? iconId, int? colourId, string secretPhrase, int? tokenNumber, DateTime createdDate, int status, int tried, string url, bool blocked, bool banned, bool isOwner, bool isDataheir, bool isWitness)
        {
            return new User(firstName, lastName, userName, password, email, age, city, countryId, maritalstatus, iconId, colourId, secretPhrase, tokenNumber, createdDate, status, tried, url, blocked, banned, isOwner, isDataheir, isWitness);
        }

        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public string Age { get; set; }

        public string City { get; set; }

        public int? CountryId { get; set; }

        public string MaritalStatus { get; set; }

        public int? IconId { get; set; }

        public int? ColourId { get; set; }

        public string SecretPhrase { get; set; }

        public int? TokenNumber { get; set; }

        public DateTime? Birthday { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int Status { get; set; }

        public int Tried { get; set; }

        public string Url { get; set; }

        public bool Blocked { get; set; }

        public bool Banned { get; set; }

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

        public bool TokenMatches(int tokenNumber)
        {
            return this.TokenNumber == tokenNumber;
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