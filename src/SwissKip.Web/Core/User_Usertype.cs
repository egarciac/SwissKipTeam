namespace SwissKip.Web.Core
{
    using System;

    public class User_UserType
    {
        public User_UserType() { }

        private User_UserType(int? userIdFather, int userId, int userTypeId, int? emailConfirmed, DateTime createdDate, int? reportedDeath, int? ownerDead, int status)
        {
            this.UserIdFather = userIdFather;
            this.UserId = userId; 
            this.UserTypeId = userTypeId;
            this.EmailConfirmed = emailConfirmed;
            this.CreatedDate = createdDate;
            this.ReportedDeath = reportedDeath;
            this.OwnerDead = ownerDead;
            this.Status = status;
        }

        public static User_UserType CreateRelationUserAndUserType(int? userIdFather, int userId, int userTypeId, int? emailConfirmed, DateTime createdDate, int? reportedDeath, int? ownerDead, int status)
        {
            return new User_UserType(userIdFather, userId, userTypeId, emailConfirmed, createdDate, reportedDeath, ownerDead, status);
        }

        public int Id { get; set; }

        public int? UserIdFather { get; set; }

        public int UserId { get; set; }

        public int UserTypeId { get; set; }

        public int? EmailConfirmed { get; set; }

        public DateTime CreatedDate { get; set; }

        public int? ReportedDeath { get; set; }
        
        public int? OwnerDead { get; set; }

        public int Status { get; set; }

    }
}