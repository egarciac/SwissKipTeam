namespace SwissKip.Web.Models
{
    using System.ComponentModel.DataAnnotations;

    using DataAnnotationsExtensions;

    public class DigitalAccountCreateModel
    {
        [Required]
        public int AccountId { get; set; }

        [Required]
        public string WebSite { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        
    }
}
