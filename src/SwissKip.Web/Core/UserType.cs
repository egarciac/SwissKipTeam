namespace SwissKip.Web.Core
{
    using System;

    public enum UserRoles
    {
        Owner = 1,
        Dataheir = 2,
        Witness = 3
    }

    public class UserType
    {
        public UserType() { }

        private UserType(string description, int status)
        {
            this.Description = description;
            this.Status = status;
        }

        public int Id { get; set; }

        public string Description { get; set; }

        public int Status { get; set; }

    }
}