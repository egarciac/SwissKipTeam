namespace SwissKip.Web.Core
{
    using System;

    public class User_UserType
    {
        public User_UserType() { }

        private User_UserType(int? userIdFather, int userId, int userTypeId, int status)
        {
            this.UserIdFather = userIdFather;
            this.UserId = userId; 
            this.UserTypeId = userTypeId; 
            this.Status = status;
        }

        public static User_UserType CreateRelationUserAndUserType(int? userIdFather, int userId, int userTypeId, int status)
        {
            return new User_UserType(userIdFather, userId, userTypeId, status);
        }

        public int Id { get; set; }

        public int? UserIdFather { get; set; }

        public int UserId { get; set; }

        public int UserTypeId { get; set; }

        public int? OwnerDied { get; set; }

        public int Status { get; set; }

    }
}