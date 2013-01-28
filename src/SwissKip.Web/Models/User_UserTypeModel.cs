namespace SwissKip.Web.Models
{
    using System.ComponentModel.DataAnnotations;

    using DataAnnotationsExtensions;

    using SwissKip.Web.Helpers.Attributes;
    using System;

    public class User_UserTypeModel
    {
        public int Id { get; set; }

        public int UserIdFather { get; set; }

        public int UserId { get; set; }

        public int UserTypeId { get; set; }

        public int EmailConfirmed { get; set; }

        public DateTime CreatedDate { get; set; }

        public int ReportedDeadth { get; set; }

        public int OwnerDead { get; set; }

        public int Status { get; set; }
    }
}