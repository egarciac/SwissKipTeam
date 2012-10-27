namespace SwissKip.Web.Models
{
    using System.ComponentModel.DataAnnotations;

    using DataAnnotationsExtensions;

    public class BankAccountModel
    {
        public string BankName { get; set; }

        public string BankAccountNumber { get; set; }

        public string Password { get; set; }

        public int? CountryId { get; set; }

        public string Country { get; set; }

    }
}