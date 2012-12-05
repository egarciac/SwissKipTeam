namespace SwissKip.Web.Models
{
    using System.ComponentModel.DataAnnotations;

    using DataAnnotationsExtensions;
using System;

    public class DigitalAccountCreateModel
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public string WebSite { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        public DateTime CreatedDate  { get; set; }

        public int Status { get; set; }
    }
}
