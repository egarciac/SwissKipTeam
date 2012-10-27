namespace SwissKip.Web.Models
{
    using System.ComponentModel.DataAnnotations;

    using DataAnnotationsExtensions;

    public class DigitalAccountModel
    {
        public int AccountId { get; set; }

        public string WebSite { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

    }
}
