namespace SwissKip.Web.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;
    
    using SwissKip.Web.Helpers.Attributes;
    using SwissKip.Web.Helpers.ModelBinders;
    using DataAnnotationsExtensions;

    public class AccountEditModel
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Birthday]
        public Birthday Birthday { get; set; }

        public int? CountryId { get; set; }

        public int? IconId { get; set; }

        public int? ColourId { get; set; }

        public string SecretPhrase { get; set; }

        public string fileName { get; set; }

        public string UserName { get; set; }

        [Required]
        [Email]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

    }

    [ModelBinder(typeof(BirthdayModelBinder))]
    public class Birthday
    {
        public int? Day { get; set; }

        public int? Month { get; set; }

        public int? Year { get; set; }

        public static Birthday CreateFrom(DateTime? birthday)
        {
            return new Birthday
                {
                    Day = birthday.HasValue ? birthday.Value.Day : 0,
                    Month = birthday.HasValue ? birthday.Value.Month : 0,
                    Year = birthday.HasValue ? birthday.Value.Year : 0
                };
        }

        public DateTime? ToDate()
        {
            if (Day == null && Month == null && Year == null) 
                return null;
            return new DateTime(Year.Value, Month.Value, Day.Value);
        }
    }
}