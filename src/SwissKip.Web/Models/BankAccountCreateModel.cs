namespace SwissKip.Web.Models
{
    using System.ComponentModel.DataAnnotations;

    using DataAnnotationsExtensions;

    public class BankAccountCreateModel
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public string BankName { get; set; }

        [Required]
        public string BankAccountNumber { get; set; }

        [Required]
        public string Password { get; set; }

        //[Required]
        //public string Country { get; set; }

        [Required]
        public int? CountryId { get; set; }

        public string BankAccountNumber1 { get; set; }
        public string Password1 { get; set; }

        public string texto1 { get; set; }
        public string texto2 { get; set; }
        public string texto3 { get; set; }
        public string texto4 { get; set; }
        public string texto5 { get; set; }
        public string texto6 { get; set; }
        public string texto7 { get; set; }
        public string texto8 { get; set; }
        public string texto9 { get; set; }
        public string texto0 { get; set; }

        public string clave1 { get; set; }
        public string clave2 { get; set; }
        public string clave3 { get; set; }
        public string clave4 { get; set; }
        public string clave5 { get; set; }
        public string clave6 { get; set; }
        
    }
} 