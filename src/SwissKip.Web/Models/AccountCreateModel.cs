namespace SwissKip.Web.Models
{
    using System.ComponentModel.DataAnnotations;

    using DataAnnotationsExtensions;

    public class AccountCreateModel
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [Email]
        public string Email { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        //public bool ReceivedEmailInvitation { get; set; }
    }
}