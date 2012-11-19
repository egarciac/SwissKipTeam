namespace SwissKip.Web.Models
{
    using System.ComponentModel.DataAnnotations;

    using DataAnnotationsExtensions;

    using SwissKip.Web.Helpers.Attributes;

    public class RecipientAddModel
    {
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [Email]
        public string Email { get; set; }
    }
}