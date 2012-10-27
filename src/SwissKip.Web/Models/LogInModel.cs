namespace SwissKip.Web.Models
{
    using System.ComponentModel.DataAnnotations;

    public class SignInModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}